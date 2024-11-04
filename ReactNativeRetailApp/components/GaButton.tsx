import { Component, ReactNode } from 'react';
import { StyleSheet, ViewStyle } from 'react-native';
import { LinearGradient } from 'expo-linear-gradient';
import { Button, Text, theme } from 'galio-framework';

import MaterialTheme from '../constants/MaterialTheme';

// Define the type for the component's props
interface GaButtonProps {
    gradient?: boolean;
    children: ReactNode;
    style?: ViewStyle;
    [key: string]: any; // Allow other props for the Button component
}

// Create a class component in TypeScript
export default class GaButton extends Component<GaButtonProps> {
    render() {
        const { gradient, children, style, ...props } = this.props;

        if (gradient) {
            return (
                <LinearGradient
                    start={{ x: 0, y: 0 }}
                    end={{ x: 1, y: 0 }}
                    locations={[0.2, 1]}
                    style={[styles.gradient, style]}
                    colors={[
                        MaterialTheme.COLORS.GRADIENT_START,
                        MaterialTheme.COLORS.GRADIENT_END,
                    ]}
                >
                    <Button color="transparent" style={[styles.gradient, style]} {...props}>
                        <Text color={theme.COLORS?.WHITE ?? '#FFFFFF'}>{children}</Text>
                    </Button>
                </LinearGradient>
            );
        }

        return <Button {...props}>{children}</Button>;
    }
}

const styles = StyleSheet.create({
    gradient: {
        borderWidth: 0,
        borderRadius: (theme.SIZES?.BASE ?? 0) * 2,
    },
});