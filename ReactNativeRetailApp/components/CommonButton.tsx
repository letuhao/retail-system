import { Component } from "react";
import { StyleSheet, Text, TouchableOpacity } from "react-native";

import Colors from "../constants/Colors";

const styles = StyleSheet.create({
    container: {
        padding: 15,
        width: "100%",
        marginBottom: 10,
        alignItems: "center",
        borderRadius: 10,
        backgroundColor: Colors.primary,
    },
    buttonText: {
        fontWeight: "bold",
        color: "#fff",
    },
    containerDisabled: {
        padding: 15,
        width: "100%",
        marginBottom: 10,
        alignItems: "center",
        borderRadius: 10,
        backgroundColor: Colors.muted,
    },
    buttonTextDisabled: {
        fontWeight: "bold",
        color: Colors.light,
    },
});

interface CommonButtonState {
    text: string;
    onPress: () => void;
    disabled?: boolean;
}

class CommonButton extends Component<CommonButtonState> {

    render() {
        const { text, onPress, disabled = false } = this.props;

        return (
            <>
                {disabled ? (
                    <TouchableOpacity
                        disabled
                        style={styles.containerDisabled}
                        onPress={onPress}
                    >
                        <Text style={styles.buttonTextDisabled}>{text}</Text>
                    </TouchableOpacity>
                ) : (
                    <TouchableOpacity style={styles.container} onPress={onPress}>
                        <Text style={styles.buttonText}>{text}</Text>
                    </TouchableOpacity>
                )}
            </>
        );
    }
}

export default CommonButton;