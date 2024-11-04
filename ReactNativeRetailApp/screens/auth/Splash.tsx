import React, { Component } from "react";
import { StyleSheet, Image, View } from "react-native";
import { NativeStackScreenProps } from '@react-navigation/native-stack';
import AsyncStorage from "@react-native-async-storage/async-storage";

import { RootStackParamList } from "../../@types/RouteParamList"
import Colors from "../../constants/Colors";

const styles = StyleSheet.create({
    container: {
        flex: 1,
        backgroundColor: Colors.primary,
        alignItems: "center",
        justifyContent: "center",
    },
    splashText: {
        color: Colors.light,
        fontSize: 50,
        fontWeight: "bold",
    },
    logo: {
        width: 80,
        height: 80,
    },
});

type SplashProps = NativeStackScreenProps<RootStackParamList, "splash">;

class Splash extends Component<SplashProps> {

    constructor(props: SplashProps) {
        super(props);
    }

    //method to fetch the user data from aync storage if there is any and login the Dashboard or Home Screen according to the user type
    retrieveData = async () => {
        try {
            const value = await AsyncStorage.getItem("authUser");

            if (value !== null) {
                let user = JSON.parse(value); // covert the user value to json
                if (user.userType === "ADMIN") {
                    setTimeout(() => {
                        this.props.navigation.navigate("dashboard", { authUser: JSON.parse(value) }); // navigate to Admin dashboard
                    }, 2000);
                } else {
                    setTimeout(() => {
                        this.props.navigation.navigate("tab", { user: JSON.parse(value) }); // navigate to User Home screen
                    }, 2000);
                }
            } else {
                setTimeout(() => {
                    this.props.navigation.navigate("login"); // // navigate to login screen if there is no authUser store in aysnc storage
                }, 2000);
            }
        } catch (error) {
            console.log(error);
        }
    };

    // Lifecycle method that runs after the component mounts
    // check the authUser and navigate to screens accordingly on initial render
    componentDidMount() {
        this.retrieveData();
    }

    render() {
        return (
            <View style={styles.container}>
                <Image style={styles.logo} source={require("../../assets/logo/logo_white.png")} resizeMode="contain" />
            </View>
        );
    }
}

export default Splash;