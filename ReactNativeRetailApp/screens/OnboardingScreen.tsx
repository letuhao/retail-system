import React, { Component } from 'react';
import { ImageBackground, StyleSheet, StatusBar, Dimensions } from 'react-native';
import { Block, Button, Text, theme } from 'galio-framework';
import { NativeStackScreenProps } from '@react-navigation/native-stack';

import { RootStackParamList } from "../@types/RouteParamList"

import MaterialTheme from '../constants/MaterialTheme';
import Images from '../constants/Images';

const { height, width } = Dimensions.get('screen');

// Define the props type
type OnboardingScreenProps = Partial<NativeStackScreenProps<RootStackParamList, "OnboardingScreen">>;

class OnboardingScreen extends Component<OnboardingScreenProps> {
    render() {
        const { navigation } = this.props;

        return (
            <Block flex style={styles.container}>
                <StatusBar barStyle="light-content" />
                <Block flex center>
                    <ImageBackground
                        source={{ uri: Images.Onboarding }}
                        style={{ height: height, width: width, marginTop: '-55%', zIndex: 1 }}
                    />
                </Block>
                <Block flex space="between" style={styles.padded}>
                    <Block flex space="around" style={{ zIndex: 2 }}>
                        <Block>
                            <Block>
                                <Text color="white" size={60}>Material</Text>
                            </Block>
                            <Block row>
                                <Text color="white" size={60}>Kit</Text>
                            </Block>
                            <Text size={16} color='rgba(255,255,255,0.6)'>
                                Fully coded React Native components.
                            </Text>
                        </Block>
                        <Block center>
                            <Button
                                shadowless
                                style={styles.button}
                                color={MaterialTheme.COLORS.BUTTON_COLOR}
                                onPress={() => navigation?.navigate('AppStack') ?? {}}>
                                GET STARTED
                            </Button>
                        </Block>
                    </Block>
                </Block>
            </Block>
        );
    }
}

export default OnboardingScreen;

const styles = StyleSheet.create({
    container: {
        backgroundColor: "black",
    },
    padded: {
        paddingHorizontal: theme.SIZES?.BASE ?? 0 * 2,
        position: 'relative',
        bottom: theme.SIZES?.BASE ?? 0,
    },
    button: {
        width: width - (theme.SIZES?.BASE ?? 0) * 4,
        height: theme.SIZES?.BASE ?? 0 * 3,
        shadowRadius: 0,
        shadowOpacity: 0,
    },
});
