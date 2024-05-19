# Understanding virtual execution environments using C#

## Homeworks and class exercises from the discipline [Virtual Execution Environments](https://www.isel.pt/en/subjects/virtual-execution-environments-leic)

[TPC list](https://github.com/isel-leic-ave/ave-2020-21-sem2-i41d/wiki/Tpc)
[Exercises 1 2 and 3 about reflection](https://github.com/isel-leic-ave/ave-2020-21-sem2-i41d/wiki/Exercicios)
[Lesson 38 exercise about reflection, interfaces and delegates](https://github.com/isel-leic-ave/ave-2020-21-sem2-i41d/wiki/Sum%C3%A1rios#lesson-38---exercise-reflection-interfaces-and-delegates)

## This discipline covers the following subjects and questions:
- What is a computational/software environment AKA virtual execution environments AKA [software framework](https://en.wikipedia.org/wiki/Software_framework)
- The problems that Virtual execution environments solve
- What is an assembly and how to get a representation of a type or component(a .dll) and exploring it
- IL([intermediate language](https://en.wikipedia.org/wiki/Common_Intermediate_Language)) + metadata = (software) component
- What is metadata for?
- The reflection API, which when in effect(in pratice) it's called introspection(which uses metadata)
- Creating our own metadata with custom attributes
- Programming in IL
- Understanding what types(reference types and value types) are how they are used during the execution of a program
- Sequences and Delegates
- Generics

___
### How to navigate and open the projects
Each folder that has the .vscode folder should be opened as a project

Open the folder and type in the explorer address bar: cmd

In the console, write: code .

Or just drag and release the folder into VSC and it will open the project

Inside each folder that contains a .csproj file for the project. Run this command in console: 
- dotnet build

Which will compile(AKA build) the .dll files and other stuff which, are files of larger size and shouldn't be placed in repos.

___

### Create projects using:

**dotnet new** *operation* **-o FolderName**

Example of operations used:
- **console** (creates project with main method)
- **classlib** (creates project intended to just contain utility classes, no main method)
- **xunit** (creates project intended to run tests on)
___

### IDE used: VSC. It's fast, simple and very good. 

### Extensions recommended for this repo using VSC:

- Name: **C# IL Viewer**
Id: josephwoodward.vscodeilviewer
Description: A C# IL Viewer for Visual Studio Code
Version: 0.0.1
Publisher: Joseph Woodward
VS Marketplace Link: https://marketplace.visualstudio.com/items?itemName=josephwoodward.vscodeilviewer

- Name: **C#**
Id: ms-dotnettools.csharp
Description: C# for Visual Studio Code (powered by OmniSharp).
Version: 1.23.12
Publisher: Microsoft
VS Marketplace Link: https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp

- Name: **vscode-pdf**
Id: tomoki1207.pdf
Description: Display pdf file in VSCode.
Version: 1.1.0
Publisher: tomoki1207
VS Marketplace Link: https://marketplace.visualstudio.com/items?itemName=tomoki1207.pdf
___
### Extra program installed: ildasm.exe
It's used to consult metadata and the IL code of .dll files.

