import React, { Component } from 'react';
import { NativeStackScreenProps } from '@react-navigation/native-stack';
import { createStackNavigator } from "@react-navigation/stack";

import { RootStackParamList } from "../@types/RouteParamList"

import AppStack from './AppStack';

import OnboardingScreen from "../screens/OnboardingScreen";

const Stack = createStackNavigator();

// Define the props type
type OnboardingStackProps = Partial<NativeStackScreenProps<RootStackParamList, "OnboardingStack">>;

class OnboardingStack extends Component<OnboardingStackProps> {
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

export default OnboardingStack;
