# IndentRainbow
### Visual Studio extension for colorization of indent levels.
### Summary
The aim of this extension is to make it easier to differentiate different levels of indentation in your source code. 
This is done by coloring the indentations in the source code.

Currently the extension assumes an indentation of 4 spaces.

### Project structure
This project has the following structure:
* src
    * Extension
    * Logic
* tests
    * Logic

The folder "/src/Extension" contains the C# Project which deals with interaction with Visual Studio. This project depends on the project "/src/Logic/".
The folder "/src/Logic" contains all the logic of the project, e.g. detection of indentation.
The folder "/tests/Logic/" contains the (Unit)tests for the "Logic" C# project. This project also depends on the "Logic" C# project.


### Inspiration
This project was inspired by this project/extension for VS Code:

[https://github.com/oderwat/vscode-indent-rainbow](https://github.com/oderwat/vscode-indent-rainbow)