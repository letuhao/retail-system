import React from 'react';
import { StyleSheet, Dimensions, ScrollView } from 'react-native';
import { Button, Block, Text, Input, theme } from 'galio-framework';
import { NativeStackScreenProps } from '@react-navigation/native-stack';

import { RootStackParamList } from "../@types/RouteParamList"

import Products from "../constants/Products";

import Product from "../components/Product";
import IconExtra from "../components/IconExtra";

const { width } = Dimensions.get('screen');

// Define the props type, assuming a navigation prop is required
type HomeScreenProps = Partial<NativeStackScreenProps<RootStackParamList, 'HomeScreen'>>;

export default class HomeScreen extends React.Component<HomeScreenProps> {
    renderSearch = () => {
        const { navigation } = this.props;
        const iconCamera = <IconExtra size={16} color={theme.COLORS?.MUTED ?? "#979797"} name="zoom-in" family="MaterialIcons" />;

        return (
            <Input
                right
                color="black"
                style={styles.search}
                iconContent={iconCamera}
                placeholder="What are you looking for?"
                onFocus={() => navigation?.navigate('Pro') ?? {}}
            />
        );
    };

    renderTabs = () => {
        const { navigation } = this.props;

        return (
            <Block row style={styles.tabs}>
                <Button shadowless style={[styles.tab, styles.divider]} onPress={() => navigation?.navigate('Pro') ?? {}}>
                    <Block row middle>
                        <IconExtra name="grid" family="Feather" style={{ paddingRight: 8 }} />
                        <Text size={16} style={styles.tabTitle}>Categories</Text>
                    </Block>
                </Button>
                <Button shadowless style={styles.tab} onPress={() => navigation?.navigate('Pro') ?? {}}>
                    <Block row middle>
                        <IconExtra size={16} name="camera-18" family="Galio" style={{ paddingRight: 8 }} />
                        <Text size={16} style={styles.tabTitle}>Best Deals</Text>
                    </Block>
                </Button>
            </Block>
        );
    };

    renderProducts = () => {
        return (
            <ScrollView
                showsVerticalScrollIndicator={false}
                contentContainerStyle={styles.products}>
                <Block flex>
                    <Product product={Products.data[0]} horizontal />
                    <Block flex row>
                        <Product product={Products.data[1]} style={{ marginRight: theme.SIZES?.BASE ?? 0 }} />
                        <Product product={Products.data[2]} />
                    </Block>
                    <Product product={Products.data[3]} horizontal />
                    <Product product={Products.data[4]} full />
                </Block>
            </ScrollView>
        );
    };

    render() {
        return (
            <Block flex center style={styles.home}>
                {this.renderProducts()}
            </Block>
        );
    }
}

const styles = StyleSheet.create({
    home: {
        width: width,
    },
    search: {
        height: 48,
        width: width - 32,
        marginHorizontal: 16,
        borderWidth: 1,
        borderRadius: 3,
    },
    header: {
        backgroundColor: theme.COLORS?.WHITE ?? "#FFFFFF",
        shadowColor: theme.COLORS?.BLACK ?? "#000000",
        shadowOffset: {
            width: 0,
            height: 2,
        },
        shadowRadius: 8,
        shadowOpacity: 0.2,
        elevation: 4,
        zIndex: 2,
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
    divider: {
        borderRightWidth: 0.3,
        borderRightColor: theme.COLORS?.MUTED ?? "#979797",
    },
    products: {
        width: width - (theme.SIZES?.BASE ?? 0) * 2,
        paddingVertical: theme.SIZES?.BASE ?? 0 * 2,
    },
});
