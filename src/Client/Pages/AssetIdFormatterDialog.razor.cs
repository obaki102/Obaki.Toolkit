
using BlazorMonaco.Editor;
using Obaki.Toolkit.Application.Features.XmlAutoEscaper.Services.TextEditor.Extensions;

namespace Obaki.Toolkit.Client.Pages;
public partial class AssetIdFormatterDialog
{

    private StandaloneCodeEditor _assetIdFormatter = default!;
    private bool _isScrippsIncluded = true;
    private bool _isDiscoveryIncluded = true;
    private bool _isAeIncluded = true;
    private bool _isOnPasteActive = true;
    private StandaloneEditorConstructionOptions EditorConstructionOptions(StandaloneCodeEditor editor)
    {
        return new StandaloneEditorConstructionOptions
        {
            AutomaticLayout = true,
        };
    }

    private async Task OnPaste(PasteEvent args)
    {
        if (_isOnPasteActive)
        {
            await AutoFormat();
        }
    }

    private async Task OnClick()
    {
        await AutoFormat();
    }

    private async Task AutoFormat()
    {
        var input = await _assetIdFormatter.GetValue();

        if (_isScrippsIncluded)
        {
            input = input.TrimScrippsAssetId();
        }

        if (_isDiscoveryIncluded)
        {
            input = input.TrimDiscoveryAssetId();
        }

        if (_isAeIncluded)
        {
            input = input.TrimAeAssetId();
        }

        await _assetIdFormatter.SetValue(input);
    }
}
