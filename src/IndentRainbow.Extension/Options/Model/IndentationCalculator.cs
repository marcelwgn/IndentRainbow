using IndentRainbow.Logic.Classification;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;

namespace IndentRainbow.Extension.Options.Model
{
    internal class IndentationCalculator
    {
        public readonly IIndentValidator indentValidator;

        private readonly IndentationSizeMode indentationSizeMode;
        private readonly IIndentationManagerService indentationService;
        public IndentationCalculator(IndentationSizeMode ism, IIndentationManagerService indentationService, string filePath, ITextBuffer textBuffer)
        {
            this.indentationService = indentationService;
            if (ism != IndentationSizeMode.Auto)
            {
                indentationSizeMode = ism;
                if (filePath != null)
                {
                    var filePathSplit = filePath.Split('.');
                    var extension = filePathSplit[filePathSplit.Length - 1];
                    if (OptionsManager.fileExtensionsDictionary.Get().ContainsKey(extension))
                    {
                        indentValidator = new IndentValidator(OptionsManager.fileExtensionsDictionary.Get()[extension]);
                    }
                }

                if (indentValidator == null)
                {
                    indentValidator = new IndentValidator(OptionsManager.indentSize.Get());
                }
            }
            else
            {
                indentValidator = new IndentValidator(indentationService.GetIndentSize(textBuffer, false));
            }
        }

        public bool ReloadIndentationSize(ITextBuffer textBuffer)
        {
            if (this.indentationSizeMode == IndentationSizeMode.Auto && indentationService != null)
            {
                // Since indentation manager might not be completely initialized yet, we need to check if the indentation has changed
                var indentSize = indentationService.GetIndentSize(textBuffer, false);
                if (indentSize != indentValidator.GetIndentBlockLength())
                {
                    indentValidator.SetIndentation(indentSize);
                    return true;
                }
            }

            return false;
        }
    }
}
