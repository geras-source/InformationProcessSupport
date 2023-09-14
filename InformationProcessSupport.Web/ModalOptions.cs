namespace InformationProcessSupport.Web
{
    public class ModalOptions
    {
        public string? PositionCustomClass { get; set; }
        public string? SizeCustomClass { get; set; }
        public string? OverlayCustomClass { get; set; }
        public string? Class { get; set; }
        public bool? DisableBackgroundCancel { get; set; }
        public bool? HideHeader { get; set; }
        public bool? HideCloseButton { get; set; }

        public bool? UseCustomLayout { get; set; }
        public bool? ActivateFocusTrap { get; set; }
    }
}
