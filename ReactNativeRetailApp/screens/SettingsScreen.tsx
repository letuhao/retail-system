import React, { Component } from 'react';
import {
    StyleSheet,
    Switch,
    FlatList,
    Platform,
    TouchableOpacity,
    View,
    ListRenderItemInfo,
} from 'react-native';
import { Block, Text, theme, Icon } from 'galio-framework';
import { NativeStackScreenProps } from '@react-navigation/native-stack';

import { RootStackParamList } from "../@types/RouteParamList"

import MaterialTheme from '../constants/MaterialTheme';

interface SettingItem {
    title: string;
    id: string;
    type: 'switch' | 'button';
}

type SettingsScreenProps = Partial<NativeStackScreenProps<RootStackParamList, 'SettingsScreen'>>;

interface SettingsScreenState {
    [key: string]: boolean | undefined;
}

class SettingsScreen extends Component<SettingsScreenProps, SettingsScreenState> {
    state: SettingsScreenState = {};

    toggleSwitch = (switchNumber: string) =>
        this.setState((prevState) => ({ [switchNumber]: !prevState[switchNumber] }));

    renderItem = ({ item }: ListRenderItemInfo<SettingItem>) => {
        const { navigation } = this.props;

        switch (item.type) {
            case 'switch':
                return (
                    <Block row middle space="between" style={styles.rows}>
                        <Text size={14}>{item.title}</Text>
                        <Switch
                            onValueChange={() => this.toggleSwitch(item.id)}
                            ios_backgroundColor={MaterialTheme.COLORS.SWITCH_OFF}
                            thumbColor={Platform.OS === 'android' ? MaterialTheme.COLORS.SWITCH_OFF : undefined}
                            trackColor={{
                                false: MaterialTheme.COLORS.SWITCH_OFF,
                                true: MaterialTheme.COLORS.SWITCH_ON,
                            }}
                            value={!!this.state[item.id]}
                        />
                    </Block>
                );
            case 'button':
                return (
                    <Block style={styles.rows}>
                        <TouchableOpacity onPress={() => navigation?.navigate('Pro') ?? {}}>
                            <Block row middle space="between" style={{ paddingTop: 7 }}>
                                <Text size={14}>{item.title}</Text>
                                <Icon name="angle-right" family="FontAwesome" style={{ paddingRight: 5 }} />
                            </Block>
                        </TouchableOpacity>
                    </Block>
                );
            default:
                return null;
        }
    };

    render() {
        const recommended: SettingItem[] = [
            { title: 'Use FaceID to sign in', id: 'face', type: 'switch' },
            { title: 'Auto-Lock security', id: 'autolock', type: 'switch' },
            { title: 'Notifications', id: 'Notifications', type: 'button' },
        ];

        const payment: SettingItem[] = [
            { title: 'Manage Payment Options', id: 'Payment', type: 'button' },
            { title: 'Manage Gift Cards', id: 'gift', type: 'button' },
        ];

        const privacy: SettingItem[] = [
            { title: 'User Agreement', id: 'Agreement', type: 'button' },
            { title: 'Privacy', id: 'Privacy', type: 'button' },
            { title: 'About', id: 'About', type: 'button' },
        ];

        return (
            <View style={styles.settings}>
                <FlatList
                    data={recommended}
                    keyExtractor={(item) => item.id}
                    renderItem={this.renderItem}
                    ListHeaderComponent={
                        <Block style={styles.title}>
                            <Text bold center size={theme.SIZES?.BASE ?? 0} style={{ paddingBottom: 5 }}>
                                Recommended Settings
                            </Text>
                            <Text center muted size={12}>
                                These are the most important settings
                            </Text>
                        </Block>
                    }
                />
                <Block style={styles.title}>
                    <Text bold center size={theme.SIZES?.BASE ?? 0} style={{ paddingBottom: 5 }}>
                        Payment Settings
                    </Text>
                    <Text center muted size={12}>
                        These are also important settings
                    </Text>
                </Block>
                <FlatList
                    data={payment}
                    keyExtractor={(item) => item.id}
                    renderItem={this.renderItem}
                />
                <Block style={styles.title}>
                    <Text bold center size={theme.SIZES?.BASE ?? 0} style={{ paddingBottom: 5 }}>
                        Privacy Settings
                    </Text>
                    <Text center muted size={12}>
                        Third most important settings
                    </Text>
                </Block>
                <FlatList
                    data={privacy}
                    keyExtractor={(item) => item.id}
                    renderItem={this.renderItem}
                />
            </View>
        );
    }
}

export default SettingsScreen;

const styles = StyleSheet.create({
    settings: {
        paddingVertical: theme.SIZES?.BASE ?? 0 / 3,
    },
    title: {
        paddingTop: theme.SIZES?.BASE ?? 0,
        paddingBottom: theme.SIZES?.BASE ?? 0 / 2,
    },
    rows: {
        height: theme.SIZES?.BASE ?? 0 * 2,
        paddingHorizontal: theme.SIZES?.BASE ?? 0,
        marginBottom: theme.SIZES?.BASE ?? 0 / 2,
    },
});
