This is an application implementing Donald Knuth's WEB in C#. The application consists of 3 parts: mTangle, mWeave and MeshIDE

1) MeshIDE is the environment in which the user can edit his or her literate program. It uses the Microsoft Windows Presentation
Foundation.

2) mTangle parses the literate program, generates C# code from it, and compiles the resulting C# code using the C# compiler.

3) mWeave generates a HTML/CSS document that contains the entire literate program with the blocks of actual Code being highlighted 
   in light blue
