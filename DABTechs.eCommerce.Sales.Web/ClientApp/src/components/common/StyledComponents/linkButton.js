import styled from "styled-components";

const LinkButton = styled.button `
    align-items: normal;
    background-color: rgba(0,0,0,0);
    bordeer-color: rgb(0,0,238);
    border-style: none;
    box-sizing: content-box;
    cursor: pointer;
    display: inline;
    font: inherit;
    height: auto;
    padding: 0;
    perspecitive-orign: 0 0;
    yexy-align: start;
    text-decoration: none;
    transform-origin: 0 0;
    width: auto;
    -moz-apperance: none;
    -webkit-logical-height: 1em;
    -webkit-logical-width: auto;

    &:hover {
        text-decoration: underline;
    }
`;

export default LinkButton;