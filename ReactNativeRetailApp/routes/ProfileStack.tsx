import React, { Component } from 'react';
import { NativeStackScreenProps } from '@react-navigation/native-stack';
import { createStackNavigator } from "@react-navigation/stack";

import { RootStackParamList } from "../@types/RouteParamList"

import Header from "../components/Header";

import ProfileScreen from "../screens/ProfileScreen";

const Stack = createStackNavigator();

// Define the props type
type ProfileStackProps = Partial<NativeStackScreenProps<RootStackParamList, "ProfileStack">>;

class ProfileStack extends Component<ProfileStackProps> {
    render(): React.ReactNode {
        return (
            <Stack.Navigator
                initialRouteName="Profile"
                screenOptions={{
                    presentation: "card",
                    headerMode: "screen",
                }}
            >
                <Stack.Screen
                    name="Profile"
                    component={ProfileScreen}
                    options={{
                        header: ({ navigation, route }) => (
                            <Header
                                white
                                transparent
                                title="Profile"
                                scene={route}
                                navigation={navigation}
                            />
                        ),
                        headerTransparent: true,
                    }}
                />
            </Stack.Navigator>
        );
    }
}

export default ProfileStack;
