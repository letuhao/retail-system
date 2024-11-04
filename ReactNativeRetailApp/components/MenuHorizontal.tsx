import React, { Component } from 'react';
import { StyleSheet, Dimensions, FlatList, Animated, FlatListProps } from 'react-native';
import { Block, theme } from 'galio-framework';

import MaterialTheme from '../constants/MaterialTheme';

const { width } = Dimensions.get('screen');

const defaultMenu = [
    { id: 'popular', title: 'Popular' },
    { id: 'beauty', title: 'Beauty' },
    { id: 'cars', title: 'Cars' },
    { id: 'motocycles', title: 'Motocycles' },
];

// Define interface for the menu item
interface MenuItem {
    id: string;
    title: string;
}

// Define props interface for MenuHorizontal
interface MenuHorizontalProps extends FlatListProps<MenuItem> {
    data: MenuItem[];
    initialIndex: number | null;
    onChange?: (id: string) => void;
}

// Define state interface for MenuHorizontal
interface MenuHorizontalState {
    active: string | null;
}

class MenuHorizontal extends Component<MenuHorizontalProps, MenuHorizontalState> {
    static defaultProps = {
        data: defaultMenu,
        initialIndex: null,
    };

    state: MenuHorizontalState = {
        active: null,
    };

    animatedValue = new Animated.Value(1);
    menuRef = React.createRef<FlatList<MenuItem>>();

    componentDidMount() {
        const { initialIndex, data } = this.props;

        // Check if data is defined and initialIndex is valid
        if (data && initialIndex !== null && initialIndex >= 0 && initialIndex < data.length) {
            this.selectMenu(data[initialIndex].id);
        }
    }

    animate() {
        this.animatedValue.setValue(0);
        Animated.timing(this.animatedValue, {
            toValue: 1,
            duration: 300,
            useNativeDriver: true, // color not supported
        }).start();
    }

    onScrollToIndexFailed = () => {
        this.menuRef.current?.scrollToIndex({
            index: 0,
            viewPosition: 0.5,
        });
    };

    selectMenu = (id: string) => {
        this.setState({ active: id });

        const index = this.props.data?.findIndex(item => item.id === id);
        if (index !== undefined && index >= 0) {
            this.menuRef.current?.scrollToIndex({
                index,
                viewPosition: 0.5,
            });
        }

        this.animate();
        this.props.onChange && this.props.onChange(id);
    };

    renderItem = ({ item }: { item: MenuItem }) => {
        const isActive = this.state.active === item.id;

        const textColor = this.animatedValue.interpolate({
            inputRange: [0, 1],
            outputRange: [
                MaterialTheme.COLORS.MUTED,
                isActive ? MaterialTheme.COLORS.ACTIVE : MaterialTheme.COLORS.MUTED,
            ],
            extrapolate: 'clamp',
        });

        const widthInterpolation = this.animatedValue.interpolate({
            inputRange: [0, 1],
            outputRange: ['0%', isActive ? '100%' : '0%'],
            extrapolate: 'clamp',
        });

        return (
            <Block style={styles.titleContainer}>
                <Animated.Text
                    style={[styles.menuTitle, { color: textColor }]}
                    onPress={() => this.selectMenu(item.id)}
                >
                    {item.title}
                </Animated.Text>
                <Animated.View style={{ height: 2, width: widthInterpolation, backgroundColor: MaterialTheme.COLORS.ACTIVE }} />
            </Block>
        );
    };

    renderMenu() {
        const { data, ...props } = this.props;

        return (
            <FlatList
                {...props}
                data={data}
                horizontal
                ref={this.menuRef}
                extraData={this.state}
                keyExtractor={(item) => item.id}
                showsHorizontalScrollIndicator={false}
                onScrollToIndexFailed={this.onScrollToIndexFailed}
                renderItem={this.renderItem}
                contentContainerStyle={styles.menu}
            />
        );
    }

    render() {
        return <Block style={[styles.container, styles.shadow]}>{this.renderMenu()}</Block>;
    }
}

export default MenuHorizontal;

const styles = StyleSheet.create({
    container: {
        width: width,
        backgroundColor: theme.COLORS?.WHITE ?? "#FFFFFF",
        zIndex: 2,
    },
    shadow: {
        shadowColor: theme.COLORS?.BLACK ?? "#000000",
        shadowOffset: { width: 0, height: 2 },
        shadowRadius: 8,
        shadowOpacity: 0.2,
        elevation: 4,
    },
    menu: {
        paddingHorizontal: theme.SIZES?.BASE ?? 0 * 2.5,
        paddingTop: 8,
        paddingBottom: 0,
    },
    titleContainer: {
        alignItems: 'center',
    },
    menuTitle: {
        fontWeight: '300',
        fontSize: 16,
        lineHeight: 28,
        paddingHorizontal: 16,
        color: MaterialTheme.COLORS.MUTED,
    },
});
