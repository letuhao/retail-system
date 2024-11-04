import React, { Component, StrictMode } from "react";
import { Platform, StatusBar, Image } from "react-native";
import { Asset } from "expo-asset";
import { Block, GalioProvider } from "galio-framework";
import { NavigationContainer } from "@react-navigation/native";
import * as SplashScreen from "expo-splash-screen";
import { enableScreens } from "react-native-screens";

import MaterialTheme from "./constants/MaterialTheme";
import Images from "./constants/Images";
import Products from "./constants/Products";

//import Screens from "./routes/Screens";
import Routes from "./routes/Routes";

enableScreens();

// cache app images
const assetImages: (string | number)[] = [
    Images.Pro,
    Images.Profile,
    Images.Avatar,
    Images.Onboarding,
];

// cache product images
Products.data.map((product) => assetImages.push(product.image));

// cache images function
const cacheImages = (images: (string | number)[]) => {
    return images.map((image) => {
        if (typeof image === "string") {
            return Image.prefetch(image);
        } else {
            return Asset.fromModule(image).downloadAsync();
        }
    });
};

interface AppState {
    appIsReady: boolean;
}

class App extends Component<{}, AppState> {
    constructor(props: {}) {
        super(props);
        this.state = {
            appIsReady: false,
        };
    }

    async componentDidMount() {
        try {
            // Load Resources
            await this.loadResourcesAsync();
        } catch (e) {
            console.warn(e);
        } finally {
            // Tell the application to render
            this.setState({ appIsReady: true });
        }
    }

    loadResourcesAsync = async () => {
        return Promise.all([...cacheImages(assetImages)]);
    };

    onLayoutRootView = async () => {
        if (this.state.appIsReady) {
            await SplashScreen.hideAsync();
        }
    };

    render() {
        const { appIsReady } = this.state;

        if (!appIsReady) {
            return null;
        }

        return (
            <StrictMode>
                <NavigationContainer onReady={this.onLayoutRootView}>
                    <GalioProvider theme={MaterialTheme}>
                        <Block flex>
                            {Platform.OS === "ios" && <StatusBar barStyle="default" />}
                            <Routes />
                        </Block>
                    </GalioProvider>
                </NavigationContainer>
            </StrictMode>
        );
    }
}

export default App;
