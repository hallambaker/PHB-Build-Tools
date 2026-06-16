namespace Goedel.Sitebuilder;

public interface IPersistSite {

    ///<summary>State management interface to keep us logged in.</summary>
    ServerCookieManager ServerCookieManager { get; set; }

    ///<summary>The Oauth Client</summary>
    OauthClient OauthClient { get; set; }

    ///<summary>The frame defintions being serviced.</summary>
    FrameSet FrameSet { get; set; }

    }
