import { Component } from 'react';
import { Switch, Platform, SwitchProps } from 'react-native';

import MaterialTheme from '../constants/MaterialTheme';

// Define the props interface
interface MkSwitchProps extends SwitchProps {
    value: boolean; // Add the value prop which is required
}

// Convert the component to a TypeScript class
class MkSwitch extends Component<MkSwitchProps> {
    render() {
        const { value, ...props } = this.props;

        const thumbColor =
            Platform.OS === 'ios'
                ? undefined
                : Platform.OS === 'android' && value
                    ? MaterialTheme.COLORS.SWITCH_ON
                    : MaterialTheme.COLORS.SWITCH_OFF;

        return (
            <Switch
                value={value}
                thumbColor={thumbColor}
                ios_backgroundColor={MaterialTheme.COLORS.SWITCH_OFF}
                trackColor={{
                    false: MaterialTheme.COLORS.SWITCH_OFF,
                    true: MaterialTheme.COLORS.SWITCH_ON,
                }}
                {...props}
            />
        );
    }
}

export default MkSwitch;