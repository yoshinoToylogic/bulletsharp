﻿using BulletSharp;
using SlimDX;
using SlimDX.Direct3D9;
using SlimDX.Windows;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace DemoFramework
{
    public class Game : System.IDisposable
    {
        public RenderForm Form
        {
            get;
            private set;
        }

        public float AspectRatio
        {
            get { return (float)Form.ClientSize.Width / (float)Form.ClientSize.Height; }
        }

        public Device Device9
        {
            get { return Context9.Device; }
        }

        /// <summary>
        /// Represents a Direct3D9 Context, only valid after calling InitializeDevice(DeviceSettings9)
        /// </summary>
        public DeviceContext9 Context9 { get; private set; }

        /// <summary>
        /// Gets the number of seconds passed since the last frame.
        /// </summary>
        public float FrameDelta { get; private set; }

        public float FramesPerSecond { get; private set; }

        private readonly Clock clock = new Clock();
        private bool deviceLost = false;
        private float frameAccumulator;
        private int frameCount;
        private FormWindowState currentFormWindowState;
        private System.IDisposable apiContext;
        private int tempWindowWidth, tempWindowHeight;

        private bool ortho = false;
        private float cameraDistance = 15.0f;
        private RigidBody pickedBody;
        private bool use6Dof = false;
        private TypedConstraint pickConstraint;
        private float mousePickClamping = 30;
        private float oldPickingDist;

        /// <summary>
        /// Disposes of object resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes of object resources.
        /// </summary>
        /// <param name="disposeManagedResources">If true, managed resources should be
        /// disposed of in addition to unmanaged resources.</param>
        protected virtual void Dispose(bool disposeManagedResources)
        {
            if (disposeManagedResources)
            {
                apiContext.Dispose();
                Form.Dispose();
            }
        }

                /// <summary>
        /// Performs object finalization.
        /// </summary>
        ~Game() {
            Dispose( false );
        }

        /// <summary>
        /// Quits the sample.
        /// </summary>
        protected void Quit()
        {
            Form.Close();
        }

        /// <summary>
        /// Updates sample state.
        /// </summary>
        private void Update() {
            FrameDelta = clock.Update();
            OnUpdate();
        }

        protected void MouseUpdate(Input input, Vector3 eye, Vector3 target, float fov, DynamicsWorld world)
        {
            if (input.MouseDown == MouseButtons.Right || input.MouseUp == MouseButtons.Right)
            {
                Vector3 rayTo = GetRayTo(input.MousePoint, eye, target, fov);

                if (input.MouseDown == MouseButtons.Right)
                {
                    if (world != null)
                    {
                        Vector3 rayFrom;
                        if (ortho)
                        {
                            rayFrom = rayTo;
                            rayFrom.Z = -100;
                        }
                        else
                        {
                            rayFrom = eye;
                        }

                        CollisionWorld.ClosestRayResultCallback rayCallback = new CollisionWorld.ClosestRayResultCallback(rayFrom, rayTo);
                        world.RayTest(rayFrom, rayTo, rayCallback);
                        if (rayCallback.HasHit)
                        {
                            RigidBody body = RigidBody.Upcast(rayCallback.CollisionObject);
                            if (body != null)
                            {
                                if (!(body.IsStaticObject() || body.IsKinematicObject()))
                                {
                                    pickedBody = body;
                                    pickedBody.ActivationState = ActivationState.DisableDeactivation;

                                    Vector3 pickPos = rayCallback.HitPointWorld;
                                    Vector3 localPivot = Vector3.TransformCoordinate(pickPos, Matrix.Invert(body.CenterOfMassTransform));

                                    if (use6Dof)
                                    {
                                        Generic6DofConstraint dof6 = new Generic6DofConstraint(body, Matrix.Translation(localPivot), false);
                                        dof6.SetLinearLowerLimit(Vector3.Zero);
                                        dof6.SetLinearUpperLimit(Vector3.Zero);
                                        dof6.SetAngularLowerLimit(Vector3.Zero);
                                        dof6.SetAngularUpperLimit(Vector3.Zero);

                                        world.AddConstraint(dof6);
                                        pickConstraint = dof6;

                                        dof6.SetParam(ConstraintParams.StopCfm, 0.8f, 0);
                                        dof6.SetParam(ConstraintParams.StopCfm, 0.8f, 1);
                                        dof6.SetParam(ConstraintParams.StopCfm, 0.8f, 2);
                                        dof6.SetParam(ConstraintParams.StopCfm, 0.8f, 3);
                                        dof6.SetParam(ConstraintParams.StopCfm, 0.8f, 4);
                                        dof6.SetParam(ConstraintParams.StopCfm, 0.8f, 5);

                                        dof6.SetParam(ConstraintParams.StopErp, 0.1f, 0);
                                        dof6.SetParam(ConstraintParams.StopErp, 0.1f, 1);
                                        dof6.SetParam(ConstraintParams.StopErp, 0.1f, 2);
                                        dof6.SetParam(ConstraintParams.StopErp, 0.1f, 3);
                                        dof6.SetParam(ConstraintParams.StopErp, 0.1f, 4);
                                        dof6.SetParam(ConstraintParams.StopErp, 0.1f, 5);
                                    }
                                    else
                                    {
                                        Point2PointConstraint p2p = new Point2PointConstraint(body, localPivot);
                                        world.AddConstraint(p2p);
                                        pickConstraint = p2p;
                                        p2p.Setting.ImpulseClamp = mousePickClamping;
                                        //very weak constraint for picking
                                        p2p.Setting.Tau = 0.001f;
                                        /*
                                        p2p.SetParam(ConstraintParams.Cfm, 0.8f, 0);
                                        p2p.SetParam(ConstraintParams.Cfm, 0.8f, 1);
                                        p2p.SetParam(ConstraintParams.Cfm, 0.8f, 2);
                                        p2p.SetParam(ConstraintParams.Erp, 0.1f, 0);
                                        p2p.SetParam(ConstraintParams.Erp, 0.1f, 1);
                                        p2p.SetParam(ConstraintParams.Erp, 0.1f, 2);
                                        */
                                    }
                                    use6Dof = !use6Dof;

                                    oldPickingDist = (pickPos - rayFrom).Length();
                                }
                            }
                        }
                    }
                }
                else if (input.MouseUp == MouseButtons.Right)
                {
                    if (pickConstraint != null && world != null)
                    {
                        world.RemoveConstraint(pickConstraint);
                        pickConstraint.Dispose();
                        pickConstraint = null;
                        pickedBody.ForceActivationState(ActivationState.ActiveTag);
                        pickedBody.DeactivationTime = 0;
                        pickedBody = null;
                    }
                }
            }

            // Mouse movement
            if (input.MouseButtons == MouseButtons.Right)
            {
                if (pickConstraint != null)
                {
                    Vector3 newRayTo = GetRayTo(input.MousePoint, eye, target, fov);

                    if (pickConstraint.ConstraintType == TypedConstraintType.D6)
                    {
                        Generic6DofConstraint pickCon = (Generic6DofConstraint)pickConstraint;

                        //keep it at the same picking distance
                        Vector3 rayFrom;
                        Vector3 scale;
                        Quaternion rotation;
                        Vector3 oldPivotInB;
                        pickCon.FrameOffsetA.Decompose(out scale, out rotation, out oldPivotInB);

                        Vector3 newPivotB;
                        if (ortho)
                        {
                            newPivotB = oldPivotInB;
                            newPivotB.X = newRayTo.X;
                            newPivotB.Y = newRayTo.Y;
                        }
                        else
                        {
                            rayFrom = eye;
                            Vector3 dir = newRayTo - rayFrom;
                            dir.Normalize();
                            dir *= oldPickingDist;

                            newPivotB = rayFrom + dir;
                        }
                        Matrix tempFrameOffsetA = pickCon.FrameOffsetA;
                        Vector4 transRow = tempFrameOffsetA.get_Rows(3);
                        transRow.X = newPivotB.X;
                        transRow.Y = newPivotB.Y;
                        transRow.Z = newPivotB.Z;
                        tempFrameOffsetA.set_Rows(3, transRow);
                        pickCon.FrameOffsetA = tempFrameOffsetA;
                    }
                    else
                    {
                        Point2PointConstraint pickCon = (Point2PointConstraint)pickConstraint;

                        //keep it at the same picking distance
                        Vector3 rayFrom;
                        Vector3 oldPivotInB = pickCon.PivotInB;
                        Vector3 newPivotB;
                        if (ortho)
                        {
                            newPivotB = oldPivotInB;
                            newPivotB.X = newRayTo.X;
                            newPivotB.Y = newRayTo.Y;
                        }
                        else
                        {
                            rayFrom = eye;
                            Vector3 dir = newRayTo - rayFrom;
                            dir.Normalize();
                            dir *= oldPickingDist;

                            newPivotB = rayFrom + dir;
                        }
                        pickCon.PivotInB = newPivotB;
                    }
                }
            }
        }

        Vector3	GetRayTo(Point point, Vector3 eye, Vector3 target, float fov)
        {
            float aspect;

            if (ortho)
            {
	            Vector3 extents;
                if (Form.ClientSize.Width > Form.ClientSize.Height) 
	            {
		            extents = new Vector3(AspectRatio, 1, 0);
	            } else 
	            {
                    aspect = (float)Form.ClientSize.Height / (float)Form.ClientSize.Width;
		            extents = new Vector3(1, aspect, 0);
	            }
            	
	            extents *= cameraDistance;
	            Vector3 lower = target - extents;
	            Vector3 upper = target + extents;

                float u = (float)point.X / (float)Form.ClientSize.Width;
                float v = (Form.ClientSize.Height - point.Y) / (float)Form.ClientSize.Width;
            	
	            Vector3 p = new Vector3((1.0f - u) * lower.X + u * upper.X, (1.0f - v) * lower.Y + v * upper.Y, target.Z);
	            return p;
            }

            Vector3 rayFrom = eye;
            Vector3 rayForward = target - eye;
            rayForward.Normalize();
            float farPlane = 10000.0f;
            rayForward *= farPlane;

            Vector3 vertical = Vector3.UnitY;

            Vector3 hor = Vector3.Cross(rayForward, vertical);
            hor.Normalize();
            vertical = Vector3.Cross(hor, rayForward);
            vertical.Normalize();

            float tanFov = (float)Math.Tan(fov/2);
            hor *= 2.0f * farPlane * tanFov;
            vertical *= 2.0f * farPlane * tanFov;

            if (Form.ClientSize.Width > Form.ClientSize.Height)
            {
                aspect = (float)Form.ClientSize.Width / (float)Form.ClientSize.Height;
	            hor *= aspect;
            } else 
            {
                aspect = (float)Form.ClientSize.Height / (float)Form.ClientSize.Width;
	            vertical *= aspect;
            }

            Vector3 rayToCenter = rayFrom + rayForward;
            Vector3 dHor = hor / (float)Form.ClientSize.Width;
            Vector3 dVert = vertical / (float)Form.ClientSize.Height;

            Vector3 rayTo = rayToCenter - 0.5f * hor + 0.5f * vertical;
            rayTo += (Form.ClientSize.Width - point.X) * dHor;
            rayTo -= point.Y * dVert;
            return rayTo;
        }

        /// <summary>
        /// Renders the game.
        /// </summary>
        private void Render()
        {
            if (deviceLost)
            {
                // This should only become true if we're using D3D9, so we can assume the
                // D3D9 context is valid at this point.
                if (Device9.TestCooperativeLevel() == SlimDX.Direct3D9.ResultCode.DeviceNotReset)
                {
                    Device9.Reset(Context9.PresentParameters);
                    deviceLost = false;
                    OnResourceLoad();
                }
                else
                {
                    Thread.Sleep(100);
                    return;
                }
            }

            frameAccumulator += FrameDelta;
            ++frameCount;
            if (frameAccumulator >= 1.0f)
            {
                FramesPerSecond = frameCount / frameAccumulator;

                frameAccumulator = 0.0f;
                frameCount = 0;
            }

            try
            {
                OnRender();
            }
            catch (SlimDX.Direct3D9.Direct3D9Exception e)
            {
                if (e.ResultCode == SlimDX.Direct3D9.ResultCode.DeviceLost)
                {
                    OnResourceUnload();
                    deviceLost = true;
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Runs the game.
        /// </summary>
        public void Run()
        {
            bool isFormClosed = false;
            bool formIsResizing = false;

            Form = new RenderForm();

            currentFormWindowState = Form.WindowState;
            Form.Resize += (o, args) =>
            {
                if (Form.WindowState != currentFormWindowState)
                {
                    HandleResize(o, args);
                }

                currentFormWindowState = Form.WindowState;
            };

            Form.ResizeBegin += (o, args) => { formIsResizing = true; };
            Form.ResizeEnd += (o, args) =>
            {
                formIsResizing = false;
                HandleResize(o, args);
            };

            Form.Closed += (o, args) => { isFormClosed = true; };

            OnInitialize();
            OnResourceLoad();

            clock.Start();
            MessagePump.Run(Form, () =>
            {
                Update();

                if (isFormClosed)
                    return;
                
                if (!formIsResizing)
                    Render();
            });

            OnResourceUnload();
        }

        /// <summary>
        /// In a derived class, implements logic to initialize the sample.
        /// </summary>
        protected virtual void OnInitialize() { }

        protected virtual void OnResourceLoad() { }

        protected virtual void OnResourceUnload() { }

        /// <summary>
        /// In a derived class, implements logic to update any relevant sample state.
        /// </summary>
        protected virtual void OnUpdate() { }

        /// <summary>
        /// In a derived class, implements logic to render the sample.
        /// </summary>
        protected virtual void OnRender() { }

        /// <summary>
        /// Initializes a <see cref="DeviceContext9">Direct3D9 device context</see> according to the specified settings.
        /// The base class retains ownership of the context and will dispose of it when appropriate.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns>The initialized device context.</returns>
        protected void InitializeDevice(DeviceSettings9 settings)
        {
            var result = new DeviceContext9(Form, settings);
            apiContext = result;
            Context9 = result;
        }

        private void HandleResize(object sender, EventArgs e)
        {
            if (Form.WindowState == FormWindowState.Minimized)
                return;

            OnResourceUnload();

            if (Context9 != null)
            {
                Context9.PresentParameters.BackBufferWidth = Form.ClientSize.Width;
                Context9.PresentParameters.BackBufferHeight = Form.ClientSize.Height;

                Device9.Reset(Context9.PresentParameters);

            }
            //else if( Context10 != null )
            //{
            //    Context10.SwapChain.ResizeBuffers( 1, WindowWidth, WindowHeight, Context10.SwapChain.Description.ModeDescription.Format, Context10.SwapChain.Description.Flags );
            //}

            OnResourceLoad();
        }

        public void ToggleFullScreen()
        {
            if (Context9 != null)
            {
                OnResourceUnload();

                if (Context9.PresentParameters.Windowed)
                {
                    tempWindowWidth = Form.ClientSize.Width;
                    tempWindowHeight = Form.ClientSize.Height;
                    Context9.PresentParameters.BackBufferWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width;
                    Context9.PresentParameters.BackBufferHeight = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height;
                    Form.ClientSize = new System.Drawing.Size(
                        Context9.PresentParameters.BackBufferWidth,
                        Context9.PresentParameters.BackBufferHeight
                        );
                }
                else
                {
                    if (tempWindowWidth > 0 && tempWindowHeight > 0)
                    {
                        Context9.PresentParameters.BackBufferWidth = tempWindowWidth;
                        Context9.PresentParameters.BackBufferHeight = tempWindowHeight;
                        Form.ClientSize = new System.Drawing.Size(
                            tempWindowWidth, tempWindowHeight);
                    }
                }
                Context9.PresentParameters.Windowed = !Context9.PresentParameters.Windowed;
                Device9.Reset(Context9.PresentParameters);
                OnResourceLoad();
            }
        }
    }
}
