import React, { Component } from 'react';
import { NativeStackScreenProps } from '@react-navigation/native-stack';
import { createStackNavigator } from "@react-navigation/stack";

import { RootStackParamList } from "../@types/RouteParamList"

import Header from "../components/Header";

import SettingsScreen from "../screens/SettingsScreen";

const Stack = createStackNavigator();

// Define the props type
type SettingsStackProps = Partial<NativeStackScreenProps<RootStackParamList, "SettingsStack">>;

class SettingsStack extends Component<SettingsStackProps> {
    render(): React.ReactNode {
        return (
            <Stack.Navigator
                initialRouteName="Settings"
                screenOptions={{
                    presentation: "card",
                    headerMode: "screen",
                }}
            >
                <Stack.Screen
                    name="Settings"
                    component={SettingsScreen}
                    options={{
                        header: ({ navigation, route }) => (
                            <Header title="Settings" navigation={navigation} scene={route} />
                        ),
                    }}
                />
            </Stack.Navigator>
        );
    }
}

export default SettingsStack;
