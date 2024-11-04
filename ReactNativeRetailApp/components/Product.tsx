import { Component } from 'react';
import { StyleSheet, Dimensions, Image, TouchableWithoutFeedback } from 'react-native';
import { Block, Text, theme } from 'galio-framework';

const { width } = Dimensions.get('screen');

interface ProductProps {
    product: {
        image: string;
        title: string;
        price: number;
    };
    horizontal?: boolean;
    full?: boolean;
    style?: object;
    priceColor?: string;
    imageStyle?: object;
    navigation?: any;
}

class Product extends Component<ProductProps> {
    render() {
        const { navigation, product, horizontal, full, style, priceColor, imageStyle } = this.props;
        const imageStyles = [styles.image, full ? styles.fullImage : styles.horizontalImage, imageStyle];

        return (
            <Block row={horizontal} card flex style={[styles.product, styles.shadow, style]}>
                <TouchableWithoutFeedback onPress={() => navigation.navigate('Pro', { product: product })}>
                    <Block flex style={[styles.imageContainer, styles.shadow]}>
                        <Image source={{ uri: product.image }} style={imageStyles} />
                    </Block>
                </TouchableWithoutFeedback>
                <TouchableWithoutFeedback onPress={() => navigation.navigate('Pro', { product: product })}>
                    <Block flex space="between" style={styles.productDescription}>
                        <Text size={14} style={styles.productTitle}>{product.title}</Text>
                        <Text size={12} muted={!priceColor} color={priceColor}>${product.price}</Text>
                    </Block>
                </TouchableWithoutFeedback>
            </Block>
        );
    }
}

export default Product;

const styles = StyleSheet.create({
    product: {
        backgroundColor: theme.COLORS?.WHITE ?? "#FFFFFF",
        marginVertical: theme.SIZES?.BASE ?? 0,
        borderWidth: 0,
        minHeight: 114,
    },
    productTitle: {
        flex: 1,
        flexWrap: 'wrap',
        paddingBottom: 6,
    },
    productDescription: {
        padding: theme.SIZES?.BASE ?? 0 / 2,
    },
    imageContainer: {
        elevation: 1,
    },
    image: {
        borderRadius: 3,
        marginHorizontal: theme.SIZES?.BASE ?? 0 / 2,
        marginTop: -16,
    },
    horizontalImage: {
        height: 122,
        width: 'auto',
    },
    fullImage: {
        height: 215,
        width: width - (theme.SIZES?.BASE ?? 0) * 3,
    },
    shadow: {
        shadowColor: theme.COLORS?.BLACK ?? "#000000",
        shadowOffset: { width: 0, height: 2 },
        shadowRadius: 4,
        shadowOpacity: 0.1,
        elevation: 2,
    },
});
