import React, { Component } from 'react';
import { NativeStackScreenProps } from '@react-navigation/native-stack';
import { createStackNavigator } from "@react-navigation/stack";

import { RootStackParamList } from "../@types/RouteParamList"

import Header from "../components/Header";

import HomeScreen from "../screens/HomeScreen";
import ProScreen from "../screens/ProScreen";

const Stack = createStackNavigator();

// Define the props type
type HomeStackProps = Partial<NativeStackScreenProps<RootStackParamList, "HomeStack">>;

class HomeStack extends Component<HomeStackProps> {
    constructor(props: HomeStackProps) {
        super(props);
    }

    render(): React.ReactNode {
        return (
            <Stack.Navigator
                screenOptions={{
                    presentation: "card",
                    headerMode: "screen",
                }}
            >
                <Stack.Screen
                    name="HomeScreen"
                    component={HomeScreen}
                    options={{
                        header: ({ navigation, route }) => (
                            <Header
                                search
                                tabs
                                title="Home"
                                navigation={navigation}
                                scene={route}
                            />
                        ),
                    }}
                />
                <Stack.Screen
                    name="ProScreen"
                    component={ProScreen}
                    options={{
                        header: ({ navigation, route }) => (
                            <Header
                                back
                                white
                                transparent
                                title=""
                                navigation={navigation}
                                scene={route}
                            />
                        ),
                        headerTransparent: true,
                    }}
                />
            </Stack.Navigator>
        );
    }
}

export default HomeStack;
