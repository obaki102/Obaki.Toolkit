using BlazorMonaco.Editor;
using MudBlazor;
using Microsoft.AspNetCore.Components;
using Obaki.Toolkit.Application.Features.XmlAutoEscaper.Services.TextEditor;
using System.Xml;
namespace Obaki.Toolkit.Client.Pages;
public partial class XmlAutoEscaper
{
    [Inject] ISnackbar? Snackbar { get; set; }
    [Inject] IDialogService? DialogService { get; set; }
    [Inject] ITextEditor<(string, string)>? TextEditor { get; set; }
    private StandaloneCodeEditor _editor = default!;

    private (string OldValue, string NewValue) value = default;
    private StandaloneEditorConstructionOptions EditorConstructionOptions(StandaloneCodeEditor editor)
    {
        return new StandaloneEditorConstructionOptions
        {
            AutomaticLayout = true,
            Language = "xml"
        };
    }
    protected override void OnInitialized()
    {
        //TODO: Undo/redo functionality
        TextEditor!.SetValue((string.Empty, string.Empty));
    }

    private async Task EditorOnDidInit()
    {
        await Global.SetTheme("vs-dark");
    }
    private void Failed(string message)
    {
        Snackbar!.Add(message, Severity.Error);
    }

    private void Succcess(string message)
    {
        Snackbar!.Add(message, Severity.Success);
    }

    private async Task AutoEscape(bool isXml)
    {
        var input = await _editor.GetValue();
        if (!IsXmlIsEmpty(input))
        {
            var escapedResult = isXml ? TextEditor!.EscapeXmlString(input) : TextEditor!.EscapeString(input);
            value = (input, escapedResult);
            await _editor.SetValue(escapedResult);
            Succcess("Special characters escaped.");

        }
    }

    private async Task ValidateXml()
    {
        var input = await _editor.GetValue();
        if (!IsXmlIsEmpty(input))
        {
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.LoadXml(input);
                Succcess("Valid XML.");
            }
            catch (XmlException)
            {
                Failed("Invalid XML");
            }

        }
    }

    private bool IsXmlIsEmpty(string xml)
    {
        if (string.IsNullOrEmpty(xml))
        {
            Failed("Please enter a valid XML string.");
            return true;
        }
        return false;
    }

    private void ShowChanges()
    {
        var options = new DialogOptions()
        {
            CloseButton = true,
            MaxWidth = MaxWidth.Large,
            FullWidth = true,
            Position = DialogPosition.Center
        };

        var parameters = new DialogParameters();
        parameters.Add("OldValue", value.OldValue);
        parameters.Add("NewValue", value.NewValue);
        DialogService!.Show<XmlAutoEscapeDialog>("Compare Changes", parameters, options);
    }
}
