"%PROGRAMFILES%\7-Zip\7z.exe" a bulletsharp.7z "Release SlimDX\bulletsharp.dll" "Release XNA 4.0\bulletsharp.dll" "Release Generic\bulletsharp.dll" "Release Axiom\bulletsharp.dll" "Release OpenTK\bulletsharp.dll" "Release SharpDX\bulletsharp.dll" "Release Mogre\bulletsharp.dll"
"%PROGRAMFILES%\7-Zip\7z.exe" a bulletsharp.zip "Release SlimDX\bulletsharp.dll" "Release XNA 4.0\bulletsharp.dll" "Release Generic\bulletsharp.dll" "Release Axiom\bulletsharp.dll" "Release OpenTK\bulletsharp.dll" "Release SharpDX\bulletsharp.dll" "Release Mogre\bulletsharp.dll"

"%PROGRAMFILES%\7-Zip\7z.exe" a bulletsharp-other.7z "Release XNA 3.1\bulletsharp.dll" "Release Windows API Code Pack\bulletsharp.dll" "Release SlimMath\bulletsharp.dll"
"%PROGRAMFILES%\7-Zip\7z.exe" a bulletsharp-other.zip "Release XNA 3.1\bulletsharp.dll" "Release Windows API Code Pack\bulletsharp.dll" "Release SlimMath\bulletsharp.dll"

cd vs2008
zip
cd ..
