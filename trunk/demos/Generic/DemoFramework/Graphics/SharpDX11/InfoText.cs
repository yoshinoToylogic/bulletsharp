using System;
using System.Globalization;
using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using Device = SharpDX.Direct3D11.Device;

namespace DemoFramework.SharpDX11
{
    public class InfoText : IDisposable
    {
        Device device;
        SharpDX.DirectWrite.Font font;
        Color4 color = new Color4(1, 0, 0, 1);
        Color4 clearColor = new Color4(1, 0, 0, 0);
        float fps = -1;
        string textString = "";
        Rectangle rect = new Rectangle(0, 0, 256, 256);
        CultureInfo culture = CultureInfo.InvariantCulture;
        Texture2D renderTexture;
        RenderTargetView renderTextureView;
        RenderTargetView[] renderViews;
        OutputMergerStage outputMerger;

        bool _isEnabled = true;
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { _isEnabled = value; }
        }

        string _text = "";
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                textString = string.Format("FPS: {0}\n{1}", fps.ToString("0.00", culture), value);
            }
        }

        public ShaderResourceView OverlayBufferRes;
        public float Width {
            get { return rect.Width; }
        }
        public float Height
        {
            get { return rect.Height; }
        }

        public InfoText(Device device)
        {
            this.device = device;
            outputMerger = device.ImmediateContext.OutputMerger;

            var dw = new SharpDX.DirectWrite.Factory(SharpDX.DirectWrite.FactoryType.Shared);
            var fonts = dw.GetSystemFontCollection(false);
            int fontIndex;
            fonts.FindFamilyName("tahoma", out fontIndex);
            var fontFamily = fonts.GetFontFamily(fontIndex);
            font = fontFamily.GetFirstMatchingFont(SharpDX.DirectWrite.FontWeight.Normal, SharpDX.DirectWrite.FontStretch.Normal, SharpDX.DirectWrite.FontStyle.Normal);

            renderTexture = new Texture2D(device, new Texture2DDescription()
            {
                ArraySize = 1,
                BindFlags = BindFlags.RenderTarget | BindFlags.ShaderResource,
                CpuAccessFlags = CpuAccessFlags.None,
                Format = Format.R8G8B8A8_UNorm,
                Height = rect.Width,
                Width = rect.Height,
                MipLevels = 1,
                OptionFlags = ResourceOptionFlags.None,
                SampleDescription = new SampleDescription(1, 0),
                Usage = ResourceUsage.Default
            });
            renderTextureView = new RenderTargetView(device, renderTexture);
            renderViews = new[] { renderTextureView };

            OverlayBufferRes = new ShaderResourceView(device, renderTexture, new ShaderResourceViewDescription()
            {
                Format = Format.R8G8B8A8_UNorm,
                Dimension = ShaderResourceViewDimension.Texture2D,
                Texture2D = new ShaderResourceViewDescription.Texture2DResource()
                {
                    MipLevels = 1,
                    MostDetailedMip = 0
                }
            });
        }

        public void Dispose()
        {
            if (renderTexture != null)
            {
                renderTexture.Dispose();
                renderTexture = null;
            }

            if (renderTextureView != null)
            {
                renderTextureView.Dispose();
                renderTextureView = null;
            }
        }

        public void OnRender(float framesPerSecond)
        {
            if (_isEnabled == false)
                return;

            if (fps != framesPerSecond)
            {
                fps = framesPerSecond;
                textString = string.Format("FPS: {0}\n{1}", fps.ToString("0.00", culture), _text);

                outputMerger.SetTargets(renderViews);
                device.ImmediateContext.ClearRenderTargetView(renderTextureView, clearColor);
                //font.DrawText(null, textString, rect, FontDrawFlags.Left, color);
            }
        }
    }
}
