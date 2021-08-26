import React from "react";
import HeaderSpacer from "./headerSpacer";
import "./logout.css";

export default class NoResults extends React.Component
{
    render() {
        return (
            <div>
                <HeaderSpacer />
                <div className="Logout">
                    <div className="no-results">
                        <p className="searched-for">
                            You have searched for <em>
                                {this.props.SearchTerm}
                            </em>
                        </p>
                        <p className="searched-for">
                            We found no results that closely match your search for &quot;{this.props.SearchTerm}&quot;
                </p>
                        <p className="suggestions">
                            Suggestions
                    <ul>
                                <li>You may have typed your word incorrectly - please check for misspellings.</li>
                                <li>You may have been too specific - please broaden your search by using fewer keywords.</li>
                                <li>Browse our products by selecting a category above.</li>
                                <li>If you know it, please search by entering a product code.</li>
                            </ul>
                        </p>
                        <p className="search-phrase">
                            Please enter a search phrase<br />
                            <input id="searchInput" type="text" value={this.props.SearchTerm} />
                            <input id="searchButton" type="button" value="Search" />
                        </p>
                    </div>
                    <div className="TitleBar">
                        <div className="Title">Shop Now</div>
                    </div>
                </div>
            </div>
        );
    }
}

/* if (ChannelId == "NEXT_GB")
            {
                var PreviewMode = Model.QueryString.ContainsKey("PreviewMode") ? Convert.ToString(Model.QueryString["PreviewMode"]) : "PreviewMode";
                @Carousel.CarouselUK(SiteURL, CDNURL, PreviewMode)
            }
            else if (!string.IsNullOrEmpty(channelObj.ChannelTheme))
            {
                <div className="SearchCarouselGap">
                    @Carousel.CarouselAlternative(SiteURL, CDNURL, Model.Model.ChannelObject.ChannelId)
                </div>
            }
            else
            {
                <div className="SearchCarouselGap">
                    @Carousel.CarouselInternational(SiteURL, CDNURL)
                </div>
            } */