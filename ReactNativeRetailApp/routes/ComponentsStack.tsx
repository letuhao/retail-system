import React, { Component } from 'react';
import { NativeStackScreenProps } from '@react-navigation/native-stack';
import { createStackNavigator } from "@react-navigation/stack";

import { RootStackParamList } from "../@types/RouteParamList"

import Header from "../components/Header";

import ScreenComponents from "../components/ScreenComponents";

const Stack = createStackNavigator();

// Define the props type
type ComponentsStackProps = Partial<NativeStackScreenProps<RootStackParamList, "ComponentsStack">>;

class ComponentsStack extends Component<ComponentsStackProps> {
    render(): React.ReactNode {
        return (
            <Stack.Navigator
                screenOptions={{
                    presentation: "card",
                    headerMode: "screen",
                }}
            >
                <Stack.Screen
                    name="screenComponents"
                    component={ScreenComponents}
                    options={{
                        header: ({ navigation, route }) => (
                            <Header title="Components" navigation={navigation} scene={route} />
                        ),
                    }}
                />
            </Stack.Navigator>
        );
    }
}

export default ComponentsStack;
