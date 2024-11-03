import React, { useState, Component } from "react";
import {
    StyleSheet,
    Image,
    Text,
    View,
    StatusBar,
    KeyboardAvoidingView,
    ScrollView,
} from "react-native";
import { NativeStackScreenProps } from '@react-navigation/native-stack';
import AsyncStorage from "@react-native-async-storage/async-storage";
import ProgressDialog from "react-native-progress-dialog";
import axios from 'axios';

import { RootStackParamList } from "../../@types/RouteParamList"
import Colors from "../../constants/Colors";
import Network from "../../constants/Network";

import CommonInput from "../../components/CommonInput";
import CommonButton from "../../components/CommonButton";
import CommonPopup from "../../components/CommonPopup";

const styles = StyleSheet.create({
    container: {
        width: "100%",
        flexDirection: "row",
        backgroundColor: Colors.light,
        alignItems: "center",
        justifyContent: "center",
        padding: 20,
        flex: 1,
    },
    welconeContainer: {
        width: "100%",
        display: "flex",
        flexDirection: "row",
        justifyContent: "space-around",
        alignItems: "center",
        height: "30%",
        // padding:15
    },
    formContainer: {
        flex: 3,
        justifyContent: "flex-start",
        alignItems: "center",
        display: "flex",
        width: "100%",
        flexDirection: "row",
        padding: 5,
    },
    logo: {
        width: 80,
    },
    welcomeText: {
        fontSize: 42,
        fontWeight: "bold",
        color: Colors.muted,
    },
    welcomeParagraph: {
        fontSize: 15,
        fontWeight: "500",
        color: Colors.primary_shadow,
    },
    forgetPasswordContainer: {
        marginTop: 10,
        width: "100%",
        display: "flex",
        flexDirection: "row",
        justifyContent: "flex-end",
        alignItems: "center",
    },
    ForgetText: {
        fontSize: 15,
        fontWeight: "600",
    },
    buttomContainer: {
        display: "flex",
        justifyContent: "center",
        width: "100%",
    },
    bottomContainer: {
        marginTop: 10,
        display: "flex",
        flexDirection: "row",
        justifyContent: "center",
    },
    signupText: {
        marginLeft: 2,
        color: Colors.primary,
        fontSize: 15,
        fontWeight: "600",
    },
    screenNameContainer: {
        marginTop: 10,
        width: "100%",
        display: "flex",
        flexDirection: "row",
        justifyContent: "flex-start",
        alignItems: "center",
    },
    screenNameText: {
        fontSize: 30,
        fontWeight: "800",
        color: Colors.muted,
    },
});

type LoginProps = NativeStackScreenProps<RootStackParamList, "login">;

interface LoginScreenState {
    email: string;
    password: string;
    errorMsg: string;
    isLoading: boolean;
}

class LoginScreen extends Component<LoginProps, LoginScreenState> {

    constructor(props: LoginProps) {
        super(props);

        this.state = {
            email: "",
            password: "",
            errorMsg: "",
            isLoading: false,
        };
    }

    //method to store the authUser to aync storage
    storeData = async (user: any) => {
        try {
            AsyncStorage.setItem("authUser", JSON.stringify(user));
        } catch (ex) {
            console.log(ex);
            this.setState({ errorMsg: String(ex) });
        }
    };

    //method to validate the user credentials and navigate to Home Screen / Dashboard
    loginHandle = () => {
        let myHeaders = new Headers();
        myHeaders.append("Content-Type", "application/json");

        let raw = JSON.stringify({
            email: this.state.email,
            password: this.state.password,
        });

        let requestOptions = {
            method: "POST",
            headers: myHeaders,
            body: raw,
            redirect: "follow",
        };

        this.setState({ isLoading: true });

        //[check validation] -- Start
        // if email does not contain @ sign
        if (this.state.email == "") {
            this.setState({ isLoading: false });
            this.setState({ errorMsg: "Please enter your email" });
            return;
        }
        if (this.state.password == "") {
            this.setState({ isLoading: false });
            this.setState({ errorMsg: "Please enter your password" });
            return;
        }
        if (!this.state.email.includes("@")) {
            this.setState({ isLoading: false });
            this.setState({ errorMsg: "Email is not valid" });
            return;
        }
        // length of email must be greater than 5 characters
        if (this.state.email.length < 6) {
            this.setState({ isLoading: false });
            this.setState({ errorMsg: "Email is too short" });
            return;
        }
        // length of password must be greater than 5 characters
        if (this.state.password.length < 6) {
            this.setState({ isLoading: false });
            this.setState({ errorMsg: "Password must be 6 characters long" });
            return;
        }
        //[check validation] -- End

        axios.post(Network.server_address + "/login", requestOptions)
            .then((response) => {
                const result = response.data; // axios automatically parses the response data

                if (
                    result.status === 200 ||
                    (result.status === 1 && result.success !== false)
                ) {
                    this.storeData(result.data); // Store user data

                    this.setState({ isLoading: false });

                    if (result?.data?.userType === "ADMIN") {
                        this.props.navigation.navigate("dashboard", { authUser: result.data }); // Navigate to Admin Dashboard
                    } else {
                        this.props.navigation.navigate("tab", { user: result.data }); // Navigate to User Dashboard
                    }
                } else {
                    this.setState({ isLoading: false, errorMsg: result.message });
                }
            })
            .catch((error) => {
                console.log("error", error.message);
                this.setState({ isLoading: false, errorMsg: error.message });
            });
    };

    setEmail = (text: string) => {
        this.setState({ email: text });
    };

    setPassword = (text: string) => {
        this.setState({ password: text });
    };

    render() {
        return (
            <KeyboardAvoidingView
                // behavior={Platform.OS === "ios" ? "padding" : "height"}
                style={styles.container}
            >
                <ScrollView style={{ flex: 1, width: "100%" }}>
                    <ProgressDialog visible={this.state.isLoading} label={"Login ..."} />
                    <StatusBar></StatusBar>
                    <View style={styles.welconeContainer}>
                        <View>
                            <Text style={styles.welcomeText}>Welcome to EasyBuy</Text>
                            <Text style={styles.welcomeParagraph}>
                                make your ecommerce easy
                            </Text>
                        </View>
                        <View>
                            <Image style={styles.logo} source={require("../../assets/logo/logo.png")} resizeMode="contain" />
                        </View>
                    </View>
                    <View style={styles.screenNameContainer}>
                        <Text style={styles.screenNameText}>Login</Text>
                    </View>
                    <View style={styles.formContainer}>
                        <CommonPopup message={this.state.errorMsg} type={"error"} />
                        <CommonInput
                            value={this.state.email}
                            setValue={this.setEmail}
                            placeholder={"Username"}
                            placeholderTextColor={Colors.muted}
                            radius={5}
                        />
                        <CommonInput
                            value={this.state.password}
                            setValue={this.setPassword}
                            secureTextEntry={true}
                            placeholder={"Password"}
                            placeholderTextColor={Colors.muted}
                            radius={5}
                        />
                        <View style={styles.forgetPasswordContainer}>
                            <Text
                                onPress={() => this.props.navigation.navigate("forgetpassword")}
                                style={styles.ForgetText}
                            >
                                Forget Password?
                            </Text>
                        </View>
                    </View>
                </ScrollView>
                <View style={styles.buttomContainer}>
                    <CommonButton text={"Login"} onPress={this.loginHandle} />
                </View>
                <View style={styles.bottomContainer}>
                    <Text>Don't have an account?</Text>
                    <Text
                        onPress={() => this.props.navigation.navigate("register")}
                        style={styles.signupText}
                    >
                        signup
                    </Text>
                </View>
            </KeyboardAvoidingView>
        );
    }
}

export default LoginScreen;
