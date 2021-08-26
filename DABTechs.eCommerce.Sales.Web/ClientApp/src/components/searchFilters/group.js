import React from "react";
import { makeStyles } from "@material-ui/core/styles";
import Accordion from "@material-ui/core/Accordion";
import AccordionSummary from "@material-ui/core/AccordionSummary";
import AccordionDetails from "@material-ui/core/AccordionDetails";
import Typography from "@material-ui/core/Typography";
import ExpandMoreIcon from "@material-ui/icons/ExpandMore";

const useStyles = makeStyles(
    (theme) => ({
        heading: {
            fontSize: theme.typography.pxToRem(14),
            fontWeight: theme.typography.fontWeightMedium,
        },
    })
);

const Group = (props) => {

    const classes = useStyles();

    return (
        <Accordion>
            <AccordionSummary expandIcon={<ExpandMoreIcon />} aria-controls="filtergroup-content" id={"filtergroup-" + props.key + "-header"}>
                <Typography className={classes.heading}>{props.name}</Typography>
            </AccordionSummary>
            <AccordionDetails>
                {props.children}
            </AccordionDetails>
        </Accordion>
    );
}

export default Group;