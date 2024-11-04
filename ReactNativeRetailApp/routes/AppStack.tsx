import React, { Component } from 'react';
import { Dimensions } from 'react-native';
import { NativeStackScreenProps } from '@react-navigation/native-stack';
import { createDrawerNavigator } from "@react-navigation/drawer";

import { RootStackParamList } from "../@types/RouteParamList"

import MaterialTheme from '../constants/MaterialTheme';
import Images from '../constants/Images';
import IconExtra from "../components/IconExtra";

import Menu from "./Menu";
import HomeStack from './HomeStack';
import ProfileStack from './ProfileStack';
import SettingsStack from './SettingsStack';
import ComponentsStack from "./ComponentsStack"

import ProScreen from "../screens/ProScreen";

const { width } = Dimensions.get("screen");

const Drawer = createDrawerNavigator();

interface Profile {
    avatar: string;
    name: string;
    type: string;
    plan: string;
    rating: number;
}

const profile: Profile = {
    avatar: Images.Profile,
    name: "Rachel Brown",
    type: "Seller",
    plan: "Pro",
    rating: 4.8,
};

// Define the props type
type AppStackProps = Partial<NativeStackScreenProps<RootStackParamList, "AppStack">>;

class AppStack extends Component<AppStackProps> {
    render(): React.ReactNode {
        return (
            <Drawer.Navigator
                drawerContent={(drawerProps) => (
                    <Menu {...drawerProps} profile={profile} />
                )}
                screenOptions={{
                    drawerStyle: {
                        backgroundColor: "white",
                        width: width * 0.8,
                    },
                    drawerActiveTintColor: 'white',
                    drawerInactiveTintColor: '#000',
                    drawerActiveBackgroundColor: MaterialTheme.COLORS.ACTIVE,
                    drawerInactiveBackgroundColor: 'transparent',
                    drawerItemStyle: {
                        width: width * 0.74,
                        paddingHorizontal: 12,
                        justifyContent: 'center',
                        alignContent: 'center',
                        overflow: 'hidden',
                    },
                    drawerLabelStyle: {
                        fontSize: 18,
                        fontWeight: 'normal',
                    },
                }}
                initialRouteName="HomeStack"
            >
                <Drawer.Screen
                    name="HomeStack"
                    component={HomeStack}
                    options={{
                        drawerIcon: ({ focused }) => (
                            <IconExtra
                                size={16}
                                name="shop"
                                family="Galio"
                                color={focused ? "white" : MaterialTheme.COLORS.MUTED}
                            />
                        ),
                    }}
                />
                <Drawer.Screen
                    name="Woman"
                    component={ProScreen}
                    options={{
                        drawerIcon: ({ focused }) => (
                            <IconExtra
                                size={16}
                                name="md-woman"
                                family="Ionicons"
                                color={focused ? "white" : MaterialTheme.COLORS.MUTED}
                                style={{ marginLeft: 4, marginRight: 4 }}
                            />
                        ),
                    }}
                />
                <Drawer.Screen
                    name="Man"
                    component={ProScreen}
                    options={{
                        drawerIcon: ({ focused }) => (
                            <IconExtra
                                size={16}
                                name="man"
                                family="Entypo"
                                color={focused ? "white" : MaterialTheme.COLORS.MUTED}
                            />
                        ),
                    }}
                />
                <Drawer.Screen
                    name="Kids"
                    component={ProScreen}
                    options={{
                        drawerIcon: ({ focused }) => (
                            <IconExtra
                                size={16}
                                name="baby"
                                family="Galio"
                                color={focused ? "white" : MaterialTheme.COLORS.MUTED}
                            />
                        ),
                    }}
                />
                <Drawer.Screen
                    name="New Collection"
                    component={ProScreen}
                    options={{
                        drawerIcon: ({ focused }) => (
                            <IconExtra
                                size={16}
                                name="grid-on"
                                family="MaterialIcons"
                                color={focused ? "white" : MaterialTheme.COLORS.MUTED}
                            />
                        ),
                    }}
                />
                <Drawer.Screen
                    name="ProfileStack"
                    component={ProfileStack}
                    options={{
                        drawerIcon: ({ focused }) => (
                            <IconExtra
                                size={16}
                                name="circle-10"
                                family="Galio"
                                color={focused ? "white" : MaterialTheme.COLORS.MUTED}
                            />
                        ),
                    }}
                />
                <Drawer.Screen
                    name="SettingsStack"
                    component={SettingsStack}
                    options={{
                        drawerIcon: ({ focused }) => (
                            <IconExtra
                                size={16}
                                name="gears"
                                family="FontAwesome"
                                color={focused ? "white" : MaterialTheme.COLORS.MUTED}
                                style={{ marginRight: -3 }}
                            />
                        ),
                    }}
                />
                <Drawer.Screen
                    name="Components"
                    component={ComponentsStack}
                    options={{
                        drawerIcon: ({ focused }) => (
                            <IconExtra
                                size={16}
                                name="md-switch"
                                family="Ionicons"
                                color={focused ? "white" : MaterialTheme.COLORS.MUTED}
                                style={{ marginRight: 2, marginLeft: 2 }}
                            />
                        ),
                    }}
                />
                <Drawer.Screen
                    name="Sign In"
                    component={ProScreen}
                    options={{
                        drawerIcon: ({ focused }) => (
                            <IconExtra
                                size={16}
                                name="ios-log-in"
                                family="Ionicons"
                                color={focused ? "white" : MaterialTheme.COLORS.MUTED}
                            />
                        ),
                    }}
                />
                <Drawer.Screen
                    name="Sign Up"
                    component={ProScreen}
                    options={{
                        drawerIcon: ({ focused }) => (
                            <IconExtra
                                size={16}
                                name="md-person-add"
                                family="Ionicons"
                                color={focused ? "white" : MaterialTheme.COLORS.MUTED}
                            />
                        ),
                    }}
                />
            </Drawer.Navigator>
        );
    }
}

export default AppStack;