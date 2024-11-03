import { Component } from "react";
import { StyleSheet, TextInput, View } from "react-native";
import { KeyboardType, DimensionValue } from "react-native"

import Colors from "../constants/Colors";

const styles = StyleSheet.create({
    CustomInput: {
        height: 40,
        marginBottom: 10,
        marginTop: 10,
        width: "100%",
        padding: 5,
        backgroundColor: Colors.white,
        elevation: 5,
        paddingHorizontal: 20,
    },
});

interface CommonInputProps {
    value: string;
    setValue: (text: string) => void;
    placeholder: string;
    secureTextEntry?: boolean;
    placeholderTextColor?: string;
    onFocus?: () => void;
    radius?: number;
    width?: DimensionValue;
    keyboardType?: KeyboardType;
    maxLength?: number;
}

class CommonInput extends Component<CommonInputProps> {
    render() {
        const {
            value,
            setValue,
            placeholder,
            secureTextEntry,
            placeholderTextColor,
            onFocus,
            radius,
            width = "100%",
            keyboardType,
            maxLength,
        } = this.props;

        return (
            <View style={{ width: width }}>
                <TextInput
                    placeholder={placeholder}
                    onChangeText={setValue}
                    value={value}
                    secureTextEntry={secureTextEntry}
                    placeholderTextColor={placeholderTextColor}
                    onFocus={onFocus}
                    maxLength={maxLength}
                    keyboardType={keyboardType}
                    style={[styles.CustomInput, { borderRadius: radius }]}
                />
            </View>
        );
    }
}

export default CommonInput;