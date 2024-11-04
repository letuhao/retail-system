import React, { Component } from 'react';
import {
    TouchableOpacity,
    StyleSheet,
    Platform,
    Dimensions,
} from 'react-native';
import { Button, Block, NavBar, Input, Text, theme } from 'galio-framework';

import IconExtra from './IconExtra';
import MaterialTheme from '../constants/MaterialTheme';
import { Route } from '@react-navigation/native';

const { height, width } = Dimensions.get('window');
const iPhoneX = () =>
    Platform.OS === 'ios' &&
    (height === 812 || width === 812 || height === 896 || width === 896);

interface Props {
    isWhite?: boolean;
    style?: any;
    navigation: any;
    back?: boolean;
    title?: string;
    white?: boolean;
    transparent?: boolean;
    search?: boolean;
    tabs?: boolean;
    tabTitleLeft?: string;
    tabTitleRight?: string;
    scene?: Route<string>;
}

interface State {
    fontLoaded: boolean;
}

class ChatButton extends Component<Props> {
    render() {
        const { isWhite, style, navigation } = this.props;
        return (
            <TouchableOpacity
                style={[styles.button, style]}
                onPress={() => navigation.navigate('Pro')}
            >
                <IconExtra
                    family="Galio"
                    size={16}
                    name="chat-33"
                    color={theme.COLORS?.[isWhite ? 'WHITE' : 'ICON'] ?? "WHITE"}
                />
                <Block middle style={styles.notify} />
            </TouchableOpacity>
        );
    }
}

class BasketButton extends Component<Props> {
    render() {
        const { isWhite, style, navigation } = this.props;
        return (
            <TouchableOpacity
                style={[styles.button, style]}
                onPress={() => navigation.navigate('Pro')}
            >
                <IconExtra
                    family="Galio"
                    size={16}
                    name="basket-simple"
                    color={theme.COLORS?.[isWhite ? 'WHITE' : 'ICON'] ?? "WHITE"}
                />
                <Block middle style={styles.notify} />
            </TouchableOpacity>
        );
    }
}

class SearchButton extends Component<Props> {
    render() {
        const { isWhite, style, navigation } = this.props;
        return (
            <TouchableOpacity
                style={[styles.button, style]}
                onPress={() => navigation.navigate('Pro')}
            >
                <IconExtra
                    size={16}
                    family="Entypo"
                    name="magnifying-glass"
                    color={theme.COLORS?.[isWhite ? 'WHITE' : 'ICON'] ?? "WHITE"}
                />
            </TouchableOpacity>
        );
    }
}

class Header extends Component<Props, State> {
    handleLeftPress = () => {
        const { back, navigation } = this.props;
        return back ? navigation.goBack() : navigation.openDrawer();
    };

    renderRight = () => {
        const { white, title, navigation } = this.props;
        if (title === 'Title') {
            return [
                <ChatButton key="chat-title" navigation={navigation} isWhite={white} />,
                <BasketButton
                    key="basket-title"
                    navigation={navigation}
                    isWhite={white}
                />,
            ];
        }

        switch (title) {
            case 'Home':
                return [
                    <ChatButton key="chat-home" navigation={navigation} isWhite={white} />,
                    <BasketButton key="basket-home" navigation={navigation} isWhite={white} />,
                ];
            case 'Deals':
                return ([
                    <ChatButton key='chat-categories' navigation={navigation} />,
                    <BasketButton key='basket-categories' navigation={navigation} />
                ]);
            case 'Categories':
                return ([
                    <ChatButton key='chat-categories' navigation={navigation} isWhite={white} />,
                    <BasketButton key='basket-categories' navigation={navigation} isWhite={white} />
                ]);
            case 'Category':
                return ([
                    <ChatButton key='chat-deals' navigation={navigation} isWhite={white} />,
                    <BasketButton key='basket-deals' navigation={navigation} isWhite={white} />
                ]);
            case 'Profile':
                return ([
                    <ChatButton key='chat-profile' navigation={navigation} isWhite={white} />,
                    <BasketButton key='basket-deals' navigation={navigation} isWhite={white} />
                ]);
            case 'Product':
                return ([
                    <SearchButton key='search-product' navigation={navigation} isWhite={white} />,
                    <BasketButton key='basket-product' navigation={navigation} isWhite={white} />
                ]);
            case 'Search':
                return ([
                    <ChatButton key='chat-search' navigation={navigation} isWhite={white} />,
                    <BasketButton key='basket-search' navigation={navigation} isWhite={white} />
                ]);
            case 'Settings':
                return ([
                    <ChatButton key='chat-search' navigation={navigation} isWhite={white} />,
                    <BasketButton key='basket-search' navigation={navigation} isWhite={white} />
                ]);
            default:
                return null;
        }
    };

    renderSearch = () => {
        const { navigation } = this.props;
        return (
            <Input
                right
                color="black"
                style={styles.search}
                placeholder="What are you looking for?"
                onFocus={() => navigation.navigate('Pro')}
                iconContent={
                    <IconExtra
                        size={16}
                        color={theme.COLORS?.MUTED ?? "#979797"}
                        name="magnifying-glass"
                        family="Entypo"
                    />
                }
            />
        );
    };

    renderTabs = () => {
        const { navigation, tabTitleLeft, tabTitleRight } = this.props;
        return (
            <Block row style={styles.tabs}>
                <Button
                    shadowless
                    style={[styles.tab, styles.divider]}
                    onPress={() => navigation.navigate('Pro')}
                >
                    <Block row middle>
                        <IconExtra name="grid" family="Feather" style={{ paddingRight: 8 }} />
                        <Text size={16} style={styles.tabTitle}>
                            {tabTitleLeft || 'Categories'}
                        </Text>
                    </Block>
                </Button>
                <Button
                    shadowless
                    style={styles.tab}
                    onPress={() => navigation.navigate('Pro')}
                >
                    <Block row middle>
                        <IconExtra
                            size={16}
                            name="camera-18"
                            family="Galio"
                            style={{ paddingRight: 8 }}
                        />
                        <Text size={16} style={styles.tabTitle}>
                            {tabTitleRight || 'Best Deals'}
                        </Text>
                    </Block>
                </Button>
            </Block>
        );
    };

    renderHeader = () => {
        const { search, tabs } = this.props;
        if (search || tabs) {
            return (
                <Block center>
                    {search ? this.renderSearch() : null}
                    {tabs ? this.renderTabs() : null}
                </Block>
            );
        }
        return null;
    };

    render() {
        const { back, title, white, transparent, navigation } = this.props;
        const noShadow = ['Search', 'Categories', 'Deals', 'Pro', 'Profile'].includes(
            title || ''
        );
        const headerStyles = [
            !noShadow ? styles.shadow : null,
            transparent ? { backgroundColor: 'rgba(0,0,0,0)' } : null,
        ];
        return (
            <Block style={headerStyles}>
                <NavBar
                    back={back}
                    title={title}
                    style={styles.navbar}
                    transparent={transparent}
                    right={this.renderRight()}
                    rightStyle={{ alignItems: 'center' }}
                    leftStyle={{ flex: 0.3, paddingTop: 2 }}
                    leftIconName={back ? 'chevron-left' : 'navicon'}
                    leftIconColor={white ? theme.COLORS?.WHITE ?? "#FFFFFF" : theme.COLORS?.ICON ?? "#FFFFFF"}
                    onLeftPress={this.handleLeftPress}
                />
                {this.renderHeader()}
            </Block>
        );
    }
}

export default Header;

const styles = StyleSheet.create({
    button: {
        padding: 12,
        position: 'relative',
    },
    title: {
        width: '100%',
        fontSize: 16,
        fontWeight: 'bold',
    },
    navbar: {
        paddingVertical: 0,
        paddingBottom: (theme.SIZES?.BASE ?? 0) * 1.5,
        paddingTop: iPhoneX() ? (theme.SIZES?.BASE ?? 0) * 4 : (theme.SIZES?.BASE ?? 1),
        zIndex: 5,
    },
    shadow: {
        backgroundColor: theme.COLORS?.WHITE ?? "#FFFFFF",
        shadowColor: 'black',
        shadowOffset: { width: 0, height: 2 },
        shadowRadius: 6,
        shadowOpacity: 0.2,
        elevation: 3,
    },
    notify: {
        backgroundColor: MaterialTheme.COLORS.LABEL,
        borderRadius: 4,
        height: (theme.SIZES?.BASE ?? 0) / 2,
        width: (theme.SIZES?.BASE ?? 0) / 2,
        position: 'absolute',
        top: 8,
        right: 8,
    },
    header: {
        backgroundColor: theme.COLORS?.WHITE ?? "#FFFFFF",
    },
    divider: {
        borderRightWidth: 0.3,
        borderRightColor: theme.COLORS?.MUTED ?? "#979797",
    },
    search: {
        height: 48,
        width: width - 32,
        marginHorizontal: 16,
        borderWidth: 1,
        borderRadius: 3,
    },
    tabs: {
        marginBottom: 24,
        marginTop: 10,
        elevation: 4,
    },
    tab: {
        backgroundColor: theme.COLORS?.TRANSPARENT ?? "#FFFFFF",
        width: width * 0.5,
        borderRadius: 0,
        borderWidth: 0,
        height: 24,
        elevation: 0,
    },
    tabTitle: {
        lineHeight: 19,
        fontWeight: '300',
    },
});
