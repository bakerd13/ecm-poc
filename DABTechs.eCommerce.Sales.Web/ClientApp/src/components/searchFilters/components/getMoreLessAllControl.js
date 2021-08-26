import React from "react";
import ViewAll from "./viewAll";
import ViewMore from "./viewMore";
import ViewLess from "./viewLess";

const GetMoreLessControl = (optionsCount, showMore, showViewHandler, minlimit = 8, maxlimit = 30) => {
    let moreOrLess = null;
    
    if (optionsCount > minlimit) {

        if (optionsCount > maxlimit)
        {
            moreOrLess = (<ViewAll onClicked={showViewHandler}></ViewAll>);
            
        }
        else {
            if (showMore){
                moreOrLess = (<ViewMore onClicked={showViewHandler}></ViewMore>);
            }
            else{
                moreOrLess = (<ViewLess onClicked={showViewHandler}></ViewLess>);
            }
        }                    
    }

    return moreOrLess
}

export default GetMoreLessControl;