import { Component } from "react";
import { TouchableOpacity, StyleSheet } from "react-native";
import { Block, Text, theme } from "galio-framework";

import IconExtra from "./IconExtra";
import MaterialTheme from "../constants/MaterialTheme";

type DrawerItemProps = {
    title: string;
    focused: boolean;
    navigation: any; // Ideally, replace `any` with the correct type for your navigation prop
};

const proScreens = [
    "Woman",
    "Man",
    "Kids",
    "New Collection",
    "Sign In",
    "Sign Up"
];

class DrawerItem extends Component<DrawerItemProps> {
    renderIcon() {
        const { title, focused } = this.props;

        const iconColor = focused ? "white" : MaterialTheme.COLORS.MUTED ?? "#979797";

        switch (title) {
            case "Home":
                return <IconExtra size={16} name="shop" family="Galio" color={iconColor} />;
            case "Woman":
                return <IconExtra size={16} name="md-woman" family="Ionicons" color={iconColor} />;
            case "Man":
                return <IconExtra size={16} name="man" family="Entypo" color={iconColor} />;
            case "Kids":
                return <IconExtra size={16} name="baby" family="Galio" color={iconColor} />;
            case "New Collection":
                return <IconExtra size={16} name="grid-on" family="MaterialIcons" color={iconColor} />;
            case "Profile":
                return <IconExtra size={16} name="circle-10" family="Galio" color={iconColor} />;
            case "Settings":
                return <IconExtra size={16} name="gears" family="FontAwesome" color={iconColor} />;
            case "Components":
                return <IconExtra size={16} name="md-switch" family="Ionicons" color={iconColor} />;
            case "Sign In":
                return <IconExtra size={16} name="ios-log-in" family="Ionicons" color={iconColor} />;
            case "Sign Up":
                return <IconExtra size={16} name="md-person-add" family="Ionicons" color={iconColor} />;
            default:
                return null;
        }
    }

    renderLabel() {
        const { title } = this.props;

        if (proScreens.includes(title)) {
            return (
                <Block middle style={styles.pro}>
                    <Text size={12} color="white">
                        PRO
                    </Text>
                </Block>
            );
        }

        return null;
    }

    render() {
        const { focused, title, navigation } = this.props;
        const proScreen = proScreens.includes(title);

        return (
            <TouchableOpacity style={{ height: 55 }} onPress={() => navigation.navigate(title)}>
                <Block
                    flex
                    row
                    style={[
                        styles.defaultStyle,
                        focused ? [styles.activeStyle, styles.shadow] : null,
                    ]}
                >
                    <Block middle flex={0.1} style={{ marginRight: 28 }}>
                        {this.renderIcon()}
                    </Block>
                    <Block row center flex={0.9}>
                        <Text
                            size={18}
                            color={
                                focused
                                    ? "white"
                                    : proScreen
                                        ? MaterialTheme.COLORS.MUTED ?? "#979797"
                                        : "black"
                            }
                        >
                            {title}
                        </Text>
                        {this.renderLabel()}
                    </Block>
                </Block>
            </TouchableOpacity>
        );
    }
}

export default DrawerItem;

const styles = StyleSheet.create({
    defaultStyle: {
        paddingVertical: 16,
        paddingHorizontal: 16,
    },
    activeStyle: {
        backgroundColor: MaterialTheme.COLORS.ACTIVE ?? "#9C26B0",
        borderRadius: 4,
    },
    shadow: {
        shadowColor: theme.COLORS?.BLACK ?? "#000000",
        shadowOffset: {
            width: 0,
            height: 2,
        },
        shadowRadius: 8,
        shadowOpacity: 0.2,
    },
    pro: {
        backgroundColor: MaterialTheme.COLORS.LABEL ?? "#FE2472",
        paddingHorizontal: 6,
        marginLeft: 8,
        borderRadius: 2,
        height: 16,
        width: 36,
    },
});
