using InformationProcessSupport.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;
using System.Reflection.Metadata;
using InformationProcessSupport.Web.Pages;

namespace InformationProcessSupport.Web.Services
{
    public class ModalService : IModalService
    {
        internal event Func<ModalReference, Task>? OnModalInstanceAdded;
        internal event Func<ModalReference, ModalResult, Task>? OnModalCloseRequested;
        public IModalReference Show<T>() where T : IComponent
            => Show<T>(string.Empty, new ModalParameters(), new ModalOptions());
        /// Shows the modal with the component type using the specified <paramref name="title"/>,
        /// passing the specified <paramref name="parameters"/>.
        /// </summary>
        /// <param name="title">Modal title.</param>
        /// <param name="parameters">Key/Value collection of parameters to pass to component being displayed.</param>
        public IModalReference Show<T>(string title, ModalParameters parameters) where T : IComponent
            => Show<T>(title, parameters, new ModalOptions());

        /// <summary>
        /// Shows the modal with the component type using the specified <paramref name="title"/>,
        /// passing the specified <paramref name="parameters"/> and setting a custom CSS style.
        /// </summary>
        /// <param name="title">Modal title.</param>
        /// <param name="parameters">Key/Value collection of parameters to pass to component being displayed.</param>
        /// <param name="options">Options to configure the modal.</param>
        public IModalReference Show<T>(string title, ModalParameters parameters, ModalOptions options) where T : IComponent
            => Show(typeof(T), title, parameters, options);

        /// <summary>
        /// Shows the modal with the component type using the specified <paramref name="title"/>,
        /// passing the specified <paramref name="parameters"/> and setting a custom CSS style.
        /// </summary>
        /// <param name="contentComponent">Type of component to display.</param>
        /// <param name="title">Modal title.</param>
        /// <param name="parameters">Key/Value collection of parameters to pass to component being displayed.</param>
        /// <param name="options">Options to configure the modal.</param>
        public IModalReference Show(Type contentComponent, string title, ModalParameters parameters, ModalOptions options)
        {
            if (!typeof(IComponent).IsAssignableFrom(contentComponent))
            {
                throw new ArgumentException($"{contentComponent.FullName} must be a Blazor Component");
            }

            ModalReference? modalReference = null;
            var modalInstanceId = Guid.NewGuid();
            var modalContent = new RenderFragment(builder =>
            {
                var i = 0;
                builder.OpenComponent(i++, contentComponent);
                foreach (var (name, value) in parameters.Parameters)
                {
                    builder.AddAttribute(i++, name, value);
                }
                builder.CloseComponent();
            });
            var modalInstance = new RenderFragment(builder =>
            {
                builder.OpenComponent<BlazoredModalInstance>(0);
                builder.SetKey("blazoredModalInstance_" + modalInstanceId);
                builder.AddAttribute(1, "Options", options);
                builder.AddAttribute(2, "Title", title);
                builder.AddAttribute(3, "Content", modalContent);
                builder.AddAttribute(4, "Id", modalInstanceId);
                builder.AddComponentReferenceCapture(5, compRef => modalReference!.ModalInstanceRef = (BlazoredModalInstance?)compRef);
                builder.CloseComponent();
            });
            modalReference = new ModalReference(modalInstanceId, modalInstance, this);

            OnModalInstanceAdded?.Invoke(modalReference);

            return modalReference;
        }

        internal void Close(ModalReference modal)
            => Close(modal, ModalResult.Ok());

        internal void Close(ModalReference modal, ModalResult result)
            => OnModalCloseRequested?.Invoke(modal, result);
    }
}
