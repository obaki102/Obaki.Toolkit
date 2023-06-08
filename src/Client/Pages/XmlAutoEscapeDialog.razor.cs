using BlazorMonaco.Editor;
using MudBlazor;
using Microsoft.AspNetCore.Components;
namespace Obaki.Toolkit.Client.Pages;
public partial class XmlAutoEscapeDialog
{
    [CascadingParameter]
    MudDialogInstance MudDialog { get; set; } = new();
    [Parameter]
    public string OldValue { get; set; } = string.Empty;
    [Parameter]
    public string NewValue { get; set; } = string.Empty;

    private StandaloneDiffEditor _diffEditor = default!;
    private StandaloneDiffEditorConstructionOptions DiffEditorConstructionOptions(StandaloneDiffEditor editor)
    {
        return new StandaloneDiffEditorConstructionOptions
        {
            OriginalEditable = false
        };
    }

    private async Task EditorOnDidInit()
    {
        // Get or create the original model
        TextModel original_model = await Global.GetModel("sample-diff-editor-originalModel");
        if (original_model == null)
        {
            original_model = await Global.CreateModel(OldValue, "xml", "sample-diff-editor-originalModel");
        }
        else
        {
            await original_model.SetValue(OldValue);
        }

        // Get or create the modified model
        TextModel modified_model = await Global.GetModel("sample-diff-editor-modifiedModel");
        if (modified_model == null)
        {
            modified_model = await Global.CreateModel(NewValue, "xml", "sample-diff-editor-modifiedModel");
        }
        else
        {
            await modified_model.SetValue(NewValue);
        }

        // Set the editor model
        await _diffEditor.SetModel(new DiffEditorModel { Original = original_model, Modified = modified_model });
    }

    
}
