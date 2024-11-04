import React from 'react';
import {
    StyleSheet,
    Dimensions,
    ScrollView,
    Image,
    ImageBackground,
    Platform,
} from 'react-native';
import { Block, Text, theme } from 'galio-framework';
import { LinearGradient } from 'expo-linear-gradient';
import { NativeStackScreenProps } from '@react-navigation/native-stack';

import { RootStackParamList } from "../@types/RouteParamList"

import Images from "../constants/Images";
import MaterialTheme from "../constants/MaterialTheme";
import Utils from "../constants/Utils";

import IconExtra from '../components/IconExtra';

const { width, height } = Dimensions.get('screen');
const thumbMeasure = (width - 48 - 32) / 3;

type ProfileScreenProps = Partial<NativeStackScreenProps<RootStackParamList, 'ProfileScreen'>>;

class ProfileScreen extends React.Component<ProfileScreenProps> {
    render() {
        const { navigation } = this.props;

        return (
            <Block flex style={styles.profile}>
                <Block flex>
                    <ImageBackground
                        source={{ uri: Images.Profile }}
                        style={styles.profileContainer}
                        imageStyle={styles.profileImage}
                    >
                        <Block flex style={styles.profileDetails}>
                            <Block style={styles.profileTexts}>
                                <Text color="white" size={28} style={{ paddingBottom: 8 }}>
                                    Rachel Brown
                                </Text>
                                <Block row space="between">
                                    <Block row>
                                        <Block middle style={styles.pro}>
                                            <Text size={16} color="white">
                                                Pro
                                            </Text>
                                        </Block>
                                        <Text color="white" size={16} muted style={styles.seller}>
                                            Seller
                                        </Text>
                                        <Text size={16} color={MaterialTheme.COLORS.WARNING}>
                                            4.8 <IconExtra name="shape-star" family="Galio" size={14} />
                                        </Text>
                                    </Block>
                                    <Block>
                                        <Text color={theme.COLORS?.MUTED ?? "#979797"} size={16}>
                                            <IconExtra name="map-marker" family="FontAwesome" color={theme.COLORS?.MUTED ?? "#979797"} size={16} />
                                            {' '} Los Angeles, CA
                                        </Text>
                                    </Block>
                                </Block>
                            </Block>
                            <LinearGradient colors={['rgba(0,0,0,0)', 'rgba(0,0,0,1)']} style={styles.gradient} />
                        </Block>
                    </ImageBackground>
                </Block>
                <Block flex style={styles.options}>
                    <ScrollView showsVerticalScrollIndicator={false}>
                        <Block row space="between" style={{ padding: theme.SIZES?.BASE ?? 0 }}>
                            <Block middle>
                                <Text bold size={12} style={{ marginBottom: 8 }}>
                                    36
                                </Text>
                                <Text muted size={12}>
                                    Orders
                                </Text>
                            </Block>
                            <Block middle>
                                <Text bold size={12} style={{ marginBottom: 8 }}>
                                    5
                                </Text>
                                <Text muted size={12}>
                                    Bids & Offers
                                </Text>
                            </Block>
                            <Block middle>
                                <Text bold size={12} style={{ marginBottom: 8 }}>
                                    2
                                </Text>
                                <Text muted size={12}>
                                    Messages
                                </Text>
                            </Block>
                        </Block>
                        <Block row space="between" style={{ paddingVertical: 16, alignItems: 'baseline' }}>
                            <Text size={16}>Recently viewed</Text>
                            <Text
                                size={12}
                                color={theme.COLORS?.PRIMARY ?? "#FFFFFF"}
                                onPress={() => navigation?.navigate('Home')}
                            >
                                View All
                            </Text>
                        </Block>
                        <Block style={{ paddingBottom: - Utils.HeaderHeight * 2 }}>
                            <Block row space="between" style={{ flexWrap: 'wrap' }}>
                                {Images.Viewed.map((img: string, imgIndex: number) => (
                                    <Image
                                        source={{ uri: img }}
                                        key={`viewed-${img}`}
                                        resizeMode="cover"
                                        style={styles.thumb}
                                    />
                                ))}
                            </Block>
                        </Block>
                    </ScrollView>
                </Block>
            </Block>
        );
    }
}

export default ProfileScreen;

const styles = StyleSheet.create({
    profile: {
        marginTop: Platform.OS === 'android' ? -Utils.HeaderHeight : 0,
        marginBottom: -Utils.HeaderHeight * 2,
    },
    profileImage: {
        width: width * 1.1,
        height: 'auto',
    },
    profileContainer: {
        width: width,
        height: height / 2,
    },
    profileDetails: {
        paddingTop: theme.SIZES?.BASE ?? 0 * 4,
        justifyContent: 'flex-end',
        position: 'relative',
    },
    profileTexts: {
        paddingHorizontal: theme.SIZES?.BASE ?? 0 * 2,
        paddingVertical: theme.SIZES?.BASE ?? 0 * 2,
        zIndex: 2,
    },
    pro: {
        backgroundColor: MaterialTheme.COLORS.LABEL,
        paddingHorizontal: 6,
        marginRight: theme.SIZES?.BASE ?? 0 / 2,
        borderRadius: 4,
        height: 19,
        width: 38,
    },
    seller: {
        marginRight: theme.SIZES?.BASE ?? 0 / 2,
    },
    options: {
        position: 'relative',
        padding: theme.SIZES?.BASE ?? 0,
        marginHorizontal: theme.SIZES?.BASE ?? 0,
        marginTop: -(theme.SIZES?.BASE ?? 0) * 7,
        borderTopLeftRadius: 13,
        borderTopRightRadius: 13,
        backgroundColor: theme.COLORS?.WHITE ?? "#FFFFFF",
        shadowColor: 'black',
        shadowOffset: { width: 0, height: 0 },
        shadowRadius: 8,
        shadowOpacity: 0.2,
        zIndex: 2,
    },
    thumb: {
        borderRadius: 4,
        marginVertical: 4,
        alignSelf: 'center',
        width: thumbMeasure,
        height: thumbMeasure,
    },
    gradient: {
        zIndex: 1,
        left: 0,
        right: 0,
        bottom: 0,
        height: '30%',
        position: 'absolute',
    },
});
