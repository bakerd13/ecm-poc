import React from "react";
import { MainRoutes } from "./routes/mainRoutes";  
import Layout from "./components/pages/layout";

class Vip extends React.Component {
    render() {
        return (
            <Layout>
                {MainRoutes}
            </Layout>
        );
    }
}

export default Vip;