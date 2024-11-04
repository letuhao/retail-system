import React, { Component } from "react";
import {
    TouchableWithoutFeedback,
    ScrollView,
    StyleSheet,
    Image,
} from "react-native";
import { Block, Text, theme } from "galio-framework";
import { SafeAreaInsetsContext } from "react-native-safe-area-context";
import { DrawerNavigationHelpers, DrawerDescriptorMap } from "@react-navigation/drawer/lib/typescript/src/types";
import { DrawerNavigationState, ParamListBase } from "@react-navigation/native";

import MaterialTheme from '../constants/MaterialTheme';

import IconExtra from "../components/IconExtra";
import DrawerItem from "../components/DrawerItem";

interface Profile {
    avatar: string;
    name: string;
    plan: string;
    type: string;
    rating: number;
}

interface MenuProps {
    drawerPosition?: "left" | "right";
    focused?: boolean;
    navigation: DrawerNavigationHelpers;
    profile: Profile;
    state: DrawerNavigationState<ParamListBase>;
    descriptors: DrawerDescriptorMap;
}

class Menu extends Component<MenuProps> {
    screens = [
        "Home",
        "Woman",
        "Man",
        "Kids",
        "New Collection",
        "Profile",
        "Settings",
        "Components"
    ];

    render() {
        const { drawerPosition, navigation, profile, state } = this.props;

        return (
            <SafeAreaInsetsContext.Consumer>
                {(insets) => (
                    <Block
                        style={styles.container}
                        forceInset={{ top: "always", horizontal: "never" }}
                    >
                        <Block flex={0.25} style={styles.header}>
                            <TouchableWithoutFeedback
                                onPress={() => navigation.navigate("Profile")}
                            >
                                <Block style={styles.profile}>
                                    <Image source={{ uri: profile.avatar }} style={styles.avatar} />
                                    <Text h5 color={"white"}>
                                        {profile.name}
                                    </Text>
                                </Block>
                            </TouchableWithoutFeedback>
                            <Block row>
                                <Block middle style={styles.pro}>
                                    <Text size={16} color="white">
                                        {profile.plan}
                                    </Text>
                                </Block>
                                <Text size={16} muted style={styles.seller}>
                                    {profile.type}
                                </Text>
                                <Text size={16} color={MaterialTheme.COLORS.WARNING}>
                                    {profile.rating}{" "}
                                    <IconExtra name="shape-star" family="Galio" size={14} />
                                </Text>
                            </Block>
                        </Block>
                        <Block flex style={{ paddingLeft: 7, paddingRight: 14 }}>
                            <ScrollView
                                contentContainerStyle={[
                                    {
                                        paddingTop: (insets?.top || 0) * 0.4,
                                        paddingLeft: drawerPosition === "left" ? insets?.left : 0,
                                        paddingRight: drawerPosition === "right" ? insets?.right : 0
                                    }
                                ]}
                                showsVerticalScrollIndicator={false}
                            >
                                {this.screens.map((item, index) => (
                                    <DrawerItem
                                        title={item}
                                        key={index}
                                        navigation={navigation}
                                        focused={state.index === index}
                                    />
                                ))}
                            </ScrollView>
                        </Block>
                        <Block flex={0.3} style={{ paddingLeft: 7, paddingRight: 14 }}>
                            <DrawerItem
                                title="Sign In"
                                navigation={navigation}
                                focused={state.index === 8}
                            />
                            <DrawerItem
                                title="Sign Up"
                                navigation={navigation}
                                focused={state.index === 9}
                            />
                        </Block>
                    </Block>
                )}
            </SafeAreaInsetsContext.Consumer>
        );
    }
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
    },
    header: {
        backgroundColor: '#4B1958',
        paddingHorizontal: 28,
        paddingBottom: theme.SIZES?.BASE ?? 0,
        paddingTop: theme.SIZES?.BASE ?? 0 * 2,
        justifyContent: 'center',
    },
    footer: {
        paddingHorizontal: 28,
        justifyContent: 'flex-end'
    },
    profile: {
        marginBottom: theme.SIZES?.BASE ?? 0 / 2,
    },
    avatar: {
        height: 40,
        width: 40,
        borderRadius: 20,
        marginBottom: theme.SIZES?.BASE ?? 0,
    },
    pro: {
        backgroundColor: MaterialTheme.COLORS.LABEL,
        paddingHorizontal: 6,
        marginRight: 8,
        borderRadius: 4,
        height: 19,
        width: 38,
    },
    seller: {
        marginRight: 16,
    }
});

export default Menu;
