using Microsoft.AspNetCore.Components;

namespace InformationProcessSupport.Web.Services.Contracts
{
    public interface IModalService
    {
        /// <summary>
        /// Shows a modal containing a <typeparamref name="TComponent"/> with the specified <paramref name="title"/> and <paramref name="parameters"/>.
        /// </summary>
        /// <param name="title">Modal title</param>
        /// <param name="parameters">Key/Value collection of parameters to pass to component being displayed</param>
        IModalReference Show<TComponent>() where TComponent : IComponent;

        IModalReference Show<TComponent>(string title, ModalParameters parameters) where TComponent : IComponent;
    }
}
