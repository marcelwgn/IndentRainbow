using Microsoft.VisualStudio.Shell;

namespace IndentRainbow.Extension.Options
{
    internal static partial class OptionsManager
    {
        internal class OptionsField<T>
        {
            private T item;
            public OptionsField(T value)
            {
                item = value;
            }
            public T Get()
            {
                ThreadHelper.ThrowIfNotOnUIThread();
                if (!loadedFromStorage)
                {
                    LoadSettings();
                }
                return item;
            }

            public void Set(T item)
            {
                this.item = item;
            }
        }

    }
}
