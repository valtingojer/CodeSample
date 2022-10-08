using System;
namespace I9P.Denuncias.Infrastructure.ViewModels
{
    public class EmailViewModel
    {
        public EmailViewModel()
        {
            FooterMiddlePreLinkText = "© 2020";
            FooterMiddleLinkUrl = "https://www.xxxx.com";
            FooterMiddleLinkText = "xxx group.";
            FooterMiddlePosLinkText = "Todos os direitos reservados";

            FooterBottomPreLinkText = "Desenvolvido por";
            FooterBottomLinkUrl = "https://www.yyyy.com";
            FooterBottomLinkText = "yyyy developer.";
            FooterBottomPosLinkText = string.Empty;
        }

        public LayoutType Layout { get; set; }

        public string IncidentId { get; set; }
        public string IncidentDate => ( !string.IsNullOrEmpty(Locale) && Locale.Contains("pt")) ? string.Format(CreatedOn.ToString("dd {0} MMMM, yyyy"), "de") : CreatedOn.ToString("MMMM dd, yyyy") ;
        public DateTime CreatedOn { get; set; }
        public string Locale { get; set; }
        public string Tenant { get; set; }

        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }

        public string HeaderLogoImage { get; set; }
        public string HeaderLogoLink { get; set; }
        public string HeaderLogoTitle { get; set; }
        public string HeaderLogoSubtitle { get; set; }
        public string HeaderLogoText { get; set; }
        public bool HasHeaderText => !string.IsNullOrEmpty(HeaderLogoTitle) || !string.IsNullOrEmpty(HeaderLogoSubtitle) || !string.IsNullOrEmpty(HeaderLogoText);

        public string BodyTitle { get; set; }
        public string BodySubtitle { get; set; }
        public string BodyText { get; set; }

        public string ButtonPrimaryText { get; set; }
        public string ButtonPrimaryLink { get; set; }
        public string ButtonSecondaryText { get; set; }
        public string ButtonSecondaryLink { get; set; }
        public bool HasButtonPrimaryOnly => !string.IsNullOrEmpty(ButtonPrimaryText) && string.IsNullOrEmpty(ButtonSecondaryText);
        public bool HasButtonSecondaryOnly => string.IsNullOrEmpty(ButtonPrimaryText) && !string.IsNullOrEmpty(ButtonSecondaryText);
        public bool HasButtonPrimaryAndSecondary => !string.IsNullOrEmpty(ButtonPrimaryText) && !string.IsNullOrEmpty(ButtonSecondaryText);

        public string FooterTopPreLinkText { get; set; }
        public string FooterTopLinkUrl { get; set; }
        public string FooterTopLinkText { get; set; }
        public string FooterTopPosLinkText { get; set; }
        public bool HasFooterTop => !string.IsNullOrEmpty(FooterTopPreLinkText) || !string.IsNullOrEmpty(FooterTopLinkUrl) || !string.IsNullOrEmpty(FooterTopLinkText) || !string.IsNullOrEmpty(FooterTopPosLinkText);

        public string FooterMiddlePreLinkText { get; set; }
        public string FooterMiddleLinkUrl { get; set; }
        public string FooterMiddleLinkText { get; set; }
        public string FooterMiddlePosLinkText { get; set; }
        public bool HasFooterMiddle => !string.IsNullOrEmpty(FooterMiddlePreLinkText) || !string.IsNullOrEmpty(FooterMiddleLinkUrl) || !string.IsNullOrEmpty(FooterMiddleLinkText) || !string.IsNullOrEmpty(FooterMiddlePosLinkText);

        public string FooterBottomPreLinkText { get; set; }
        public string FooterBottomLinkUrl { get; set; }
        public string FooterBottomLinkText { get; set; }
        public string FooterBottomPosLinkText { get; set; }
        public bool HasFooterBottom => !string.IsNullOrEmpty(FooterBottomPreLinkText) || !string.IsNullOrEmpty(FooterBottomLinkUrl) || !string.IsNullOrEmpty(FooterBottomLinkText) || !string.IsNullOrEmpty(FooterBottomPosLinkText);

        public enum LayoutType : int
        {
            Default
        }
    }
}
