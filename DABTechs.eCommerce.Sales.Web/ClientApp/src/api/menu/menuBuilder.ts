// TODO how is this effected if we go international my be we need a channel discovery api.

export default class MenuBuilder {
    GetMenuApiUrl = ():string => {
        if(process.env.REACT_APP_MENUAPI_URL)
        {
            return process.env.REACT_APP_MENUAPI_URL;
        }
        else{
            const hostname = window.location.hostname;
            const protocol = window.location.protocol;

            return protocol + "\\" + "menu.api." + hostname;
        }
        
    }

    GetMenuEndpoint = ():string => {
        return this.GetMenuApiUrl() + process.env.REACT_APP_MENUAPI_PATH;
    }

}