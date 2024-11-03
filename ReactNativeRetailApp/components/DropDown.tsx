import { Component } from 'react';
import { StyleSheet, ViewStyle } from 'react-native';
import ModalDropdown from 'react-native-modal-dropdown';
import { Block, Text } from 'galio-framework';

import IconExtra from './IconExtra';

// Define the types for the component props
interface DropDownProps {
    onSelect?: (index: number, value: number | string) => void; // Optional function prop
    style?: ViewStyle; // Optional style prop
    options: (number | string)[]; // Dropdown options
}

// Define the state interface
interface DropDownState {
    value: number | string; // The selected value
}

class DropDown extends Component<DropDownProps, DropDownState> {
    // Initialize state
    state: DropDownState = {
        value: this.props.options[0] || 1, // Default to the first option or 1
    };

    // Handle the selection of an item from the dropdown
    handleOnSelect = (index: string, option: string) => {
        const { onSelect } = this.props;

        this.setState({ value: option });
        if (onSelect) {
            onSelect(Number(index), option);
        }
    }

    render() {
        const { onSelect, style, options, ...props } = this.props; // Destructure props
        return (
            <ModalDropdown
                style={[styles.qty, style]} // Apply styles
                onSelect={this.handleOnSelect} // Set the onSelect handler
                dropdownStyle={styles.dropdown} // Set dropdown styles
                dropdownTextStyle={{ paddingLeft: 16, fontSize: 12 }} // Set dropdown text styles
                {...props}
            >
                <Block flex row middle space="between"> // Dropdown content
                    <Text size={12}>{this.state.value}</Text> // Display selected value
                    <IconExtra name="angle-down" family="FontAwesome" size={11} /> // Dropdown icon
                </Block>
            </ModalDropdown>
        );
    }
}

export default DropDown;

const styles = StyleSheet.create({
    qty: {
        width: 100,
        backgroundColor: '#DCDCDC',
        paddingHorizontal: 16,
        paddingTop: 10,
        paddingBottom: 9.5,
        borderRadius: 3,
        shadowColor: 'rgba(0, 0, 0, 0.1)',
        shadowOffset: { width: 0, height: 2 },
        shadowRadius: 4,
        shadowOpacity: 1,
    },
    dropdown: {
        marginTop: 8,
        marginLeft: -16,
        width: 100,
    },
});
