using Etch.OrchardCore.ContentPermissions.Models;
using Etch.OrchardCore.ContentPermissions.ViewModels;
using OrchardCore.ContentManagement.Metadata.Models;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using System.Threading.Tasks;

namespace Etch.OrchardCore.ContentPermissions.Settings
{
    public class ContentPermissionsPartSettingsDisplayDriver : ContentTypePartDefinitionDisplayDriver
    {
        public override IDisplayResult Edit(ContentTypePartDefinition model, BuildEditorContext context)
        {
            if (!string.Equals(nameof(ContentPermissionsPart), model.PartDefinition.Name))
            {
                return null;
            }

            return Initialize<ContentPermissionsPartSettingsViewModel>("ContentPermissionsPartSettings_Edit", viewModel =>
            {
                var settings = model.GetSettings<ContentPermissionsPartSettings>();
                viewModel.RedirectUrl = settings.RedirectUrl;
            }).Location("Content");
        }

        public override async Task<IDisplayResult> UpdateAsync(ContentTypePartDefinition model, UpdateTypePartEditorContext context)
        {
            if (!string.Equals(nameof(ContentPermissionsPart), model.PartDefinition.Name))
            {
                return null;
            }

            var viewModel = new ContentPermissionsPartSettingsViewModel();

            await context.Updater.TryUpdateModelAsync(viewModel, Prefix, m => m.RedirectUrl);

            context.Builder.WithSettings(new ContentPermissionsPartSettings { RedirectUrl = viewModel.RedirectUrl });

            return Edit(model, context);
        }
    }
}
