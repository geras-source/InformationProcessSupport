namespace InformationProcessSupport.Web.Services.Contracts
{
    public interface IModalReference
    {
        Task<ModalResult> Result { get; }

        void Close();
        void Close(ModalResult result);
    }
}
