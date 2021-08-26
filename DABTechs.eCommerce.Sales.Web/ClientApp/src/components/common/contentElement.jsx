import React from "react";

export const videoContent = (link) => {
    return (
        <div>
            <video width="100%" height="100%" controls autoPlay loop muted>
                <source src={link} type="video/mp4" />
                Your browser does not support the video tag
            </video>
        </div>
        
    );
}

export const htmlContent = (htmlString) => {
    return (
        <div dangerouslySetInnerHTML={{ __html: htmlString }}>
        </div>
        
    );
}

export const externalHtmlContent = (link) => {
    const contentHtml = `<span>to do ${link}</span>`;
    return (
        <div dangerouslySetInnerHTML={{ __html: contentHtml }}>
        </div>
        
    );
}

export const imageContent = (link) => {
    return (
        <div>
            <img src={link} alt="Image"></img>
        </div>
        
    );
}