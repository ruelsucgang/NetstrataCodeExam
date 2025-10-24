SharpToScript (Coding Exam):
A simple C# console app that converts a C# class definition into its TypeScript interface equivalent.

How it works:
The program reads a sample C# class definition (stored in SourceInput.cs), parses it, and generates the matching TypeScript interface structure.
It supports one level of nested classes and handles nullable fields, lists, and primitive data types like string, int, and long

Project structure:
Core/Abstractions: Interfaces for all core services
Core/Models: Data models used by the parser and converter
Services/Helpers:Utility classes like case and type converters
Services/Parsing:Parses the C# class definition
Services/Conversion: Generates the TypeScript output
SourceInput.cs: The editable file where you can change the sample input

How to run:
Open the solution in Visual Studio 2022
Build the project
Run the console app
The generated TypeScript output will appear in the console window

Notes:
You can modify the input in SourceInput.cs to test different class structures.
The app uses dependency injection for cleaner architecture and easier maintenance.


