import { Component } from "react";
import { StyleSheet, Text, View } from "react-native";

import Colors from "../constants/Colors";

const styles = StyleSheet.create({
    alertContainer: {
        padding: 5,
        marginTop: 5,
        width: "100%",
        alignItems: "center",
        justifyContent: "center",
        opacity: 0.4,
        textAlign: "center",
    },
    alertContainer_error: {
        backgroundColor: Colors.danger,
    },
    alertContainer_success: {
        backgroundColor: Colors.success,
    },
});

interface CustomAlertProps {
    message: string;
    type: "error" | "success";
}

class CommonPopup extends Component<CustomAlertProps> {
    render() {
        const { message, type } = this.props;

        return (
            <View style={{ width: "100%" }}>
                {message !== "" ? (
                    <View style={[styles.alertContainer, styles[`alertContainer_${type}`]]}>
                        <Text>{message}</Text>
                    </View>
                ) : null}
            </View>
        );
    }
}

export default CommonPopup;
