import styled from "styled-components";
import {Grid} from "@material-ui/core";
import {breakpoints} from "@next/themes";

const ResultsGrid = styled(Grid)`
    padding: 0 1.5rem 0 1.5rem;
    margin-top: 0.75rem;

    @media (max-width: ${breakpoints.values.xl - 1}px) {
        padding: 0 1rem 0 1rem;
    }
    
    @media (max-width: ${breakpoints.values.lg - 1}px) {
        margin-top: 1.25rem;
    }
    
    @media (max-width: ${breakpoints.values.md - 1}px) {
        padding: 0 0.5rem 0 0.5rem;
        margin-top: 0.75rem;
    }
`;

export default ResultsGrid;