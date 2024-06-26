# IndentRainbow
<p align="center">
    <img src="./docs/IndentRainbow.png">
</p>

### Visual Studio extension for colorization of indent levels.
You can download the latest version of this extension on the [Visual Studio Marketplace](https://marketplace.visualstudio.com/items?itemName=chingucoding.IndentRainbow).
Screenshots of the extension can be found in <a></a>[this folder](./docs) or on the extension page linked above.

### Summary
The aim of this extension is to make it easier to differentiate different levels of indentation in your source code. 
This is done by coloring the indentations in the source code.

### Project structure
This project has the following structure:
* src
    * IndentRainbow.Extension <- VSIX Extension
    * IndentRainbow.Logic <- Core logic of extension
* tests
    * IndentRainbow.Logic.Tests <- Unit tests for the core logic.

To build the project, you will need to install the Visual Studio 2022 and "Visual Studio extension development" features.

### Inspiration
This project was inspired by this project/extension for VS Code:

[https://github.com/oderwat/vscode-indent-rainbow](https://github.com/oderwat/vscode-indent-rainbow)
