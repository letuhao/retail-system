import React, { Component } from 'react';
import { NavigationContainer } from "@react-navigation/native";
import { createNativeStackNavigator } from "@react-navigation/native-stack";

import { RootStackParamList } from "../@types/RouteParamList"

import AppStack from "./AppStack";

import Splash from "../screens/auth/Splash";
import LoginScreen from "../screens/auth/LoginScreen";
import OnboardingScreen from "../screens/OnboardingScreen";
//import RegisterScreen from "../screens/auth/RegisterScreen";
//import ForgetPasswordScreen from "../screens/auth/ForgetPasswordScreen";
//import UpdatePasswordScreen from "../screens/profile/UpdatePasswordScreen";

const Stack = createNativeStackNavigator<RootStackParamList>();

class Routes extends Component {
    // render() {
    //     return (
    //         <Stack.Navigator
    //             screenOptions={{
    //                 presentation: "card",
    //                 headerShown: false,
    //             }}
    //             initialRouteName="OnboardingScreen"
    //         >
    //             <Stack.Screen
    //                 name="OnboardingScreen"
    //                 component={OnboardingScreen}
    //                 options={{
    //                     headerTransparent: true,
    //                 }}
    //             />
    //             {/*
    //                 <Stack.Screen name="splash" component={Splash} />
    //                 <Stack.Screen name="login" component={LoginScreen} />
    //                 <Stack.Screen name="register" component={RegisterScreen} />
    //                 <Stack.Screen name="forgetpassword" component={ForgetPasswordScreen} />
    //                 <Stack.Screen name="updatepassword" component={UpdatePasswordScreen} />
    //             */}
    //         </Stack.Navigator>
    //     );
    // }

    render(): React.ReactNode {
        return (
            <Stack.Navigator
                screenOptions={{
                    presentation: "card",
                    headerShown: false,
                }}
            >
                <Stack.Screen
                    name="OnboardingScreen"
                    component={OnboardingScreen}
                    options={{
                        headerTransparent: true,
                    }}
                />
                <Stack.Screen name="AppStack" component={AppStack} />
            </Stack.Navigator>
        );
    }
}

export default Routes;
