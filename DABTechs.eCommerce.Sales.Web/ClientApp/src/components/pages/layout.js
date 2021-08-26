import React, { useState } from "react";
import Wrapper from "../common/Wrapper";
import Backdrop from "@material-ui/core/Backdrop";
import { useTheme } from "@material-ui/core/styles";
import useMediaQuery from "@material-ui/core/useMediaQuery";
import Grid from "@material-ui/core/Grid";
import Toolbar from "@material-ui/core/Toolbar";
import { makeStyles } from "@material-ui/core/styles"

import Header from "../header/Header";
import Menu from "../navigation/Menu";


const Layout = (props) => {
    const [open , setOpen] = useState(false);

    const useBackdropStyles = makeStyles({
        backdrop: {
            zIndex: 10000,
            color: "#fff",
        }
    });

    const closeBackdropHandler = () => {
        setOpen(false);
    }

    const backdropToggleHandler = () => {
        setOpen(!open);
    }

    const renderDestopMenu = (
        <Wrapper>
            <Menu backdropOpen={backdropToggleHandler} backdropClickHandler={closeBackdropHandler} />
        </Wrapper>
    );

    const theme = useTheme();
    const backdropClasses = useBackdropStyles();
    const isDesktop = useMediaQuery(theme.breakpoints.up("md"));

        return (
            <Wrapper>
                <Grid container direction="column" justify="flex-start" alignItems="stretch">
                    <Grid item>
                        <Header  />
                        <Toolbar />
                    </Grid>
                    <Grid item>
                        {isDesktop ?  renderDestopMenu : null}
                    </Grid>
                    <Grid item>
                        {props.children}                                  
                    </Grid>
                </Grid>
                <Backdrop className={backdropClasses.backdrop} open={open} onClick={closeBackdropHandler} />
            </Wrapper>
        );

}

export default Layout


