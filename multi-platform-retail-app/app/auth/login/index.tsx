import { Component } from 'react'
import { Button, Input, Stack, YStack, Text, XStack } from 'tamagui'
import { Link } from 'expo-router'
import { FontAwesome } from '@expo/vector-icons'
import axios from 'axios'

interface LoginState {
    email: string
    password: string
    showPassword: boolean
    errorMessage: string
}

export default class LoginScreen extends Component<{}, LoginState> {
    constructor(props: {}) {
        super(props)
        this.state = {
            email: '',
            password: '',
            showPassword: false,
            errorMessage: '',
        }
    }

    togglePasswordVisibility = () => {
        this.setState((prevState) => ({
            showPassword: !prevState.showPassword,
        }))
    }

    handleLogin = async () => {
        const { email, password } = this.state
        try {
            const response = await axios.post('YOUR_BACKEND_API_URL/login', {
                email,
                password,
            })
            console.log('Login successful:', response.data)
            // Handle successful login, e.g., navigate to the home screen
        } catch (error) {
            this.setState({
                errorMessage: 'Login failed. Please check your credentials.',
            })
        }
    }

    handleSocialLogin = (provider: string) => {
        console.log(`Logging in with ${provider}`)
        // Handle social login logic for each provider
    }

    render() {
        const { showPassword, errorMessage } = this.state;

        return (
            <YStack
                f={1}
                jc="center"
                ai="center"
                bg="$background"
                padding="$4"
            >
                <YStack
                    maxWidth={400}
                    width="100%"
                    alignSelf="center"
                    ai="center"
                    gap="$4"
                >
                    <Text fontSize={24} fontWeight="bold" color="$color" mb="$4">
                        Login
                    </Text>
                    <Stack gap="$4" width="100%" mt="$4">
                        <Input
                            placeholder="Email"
                            value={this.state.email}
                            onChangeText={(text) => this.setState({ email: text })}
                            keyboardType="email-address"
                            autoCapitalize="none"
                        />
                        <XStack ai="center" gap="$2">
                            <Input
                                placeholder="Password"
                                value={this.state.password}
                                onChangeText={(text) => this.setState({ password: text })}
                                secureTextEntry={!showPassword}
                                width="90%"
                            />
                            <Button onPress={this.togglePasswordVisibility}>
                                <FontAwesome
                                    name={showPassword ? 'eye-slash' : 'eye'}
                                    size={24}
                                    color="$color"
                                />
                            </Button>
                        </XStack>
                        <Button onPress={this.handleLogin}>
                            <Text color="$color">Log In</Text>
                        </Button>
                        {errorMessage ? (
                            <Text color="red" fontSize={14}>
                                {errorMessage}
                            </Text>
                        ) : null}
                    </Stack>
                    <Text color="$color" mt="$4" mb="$2">
                        Or login with:
                    </Text>
                    <XStack gap="$4">
                        <Button onPress={() => this.handleSocialLogin('Facebook')}>
                            <FontAwesome name="facebook" size={32} color="#4267B2" />
                        </Button>
                        <Button onPress={() => this.handleSocialLogin('Google')}>
                            <FontAwesome name="google" size={32} color="#DB4437" />
                        </Button>
                        <Button onPress={() => this.handleSocialLogin('X')}>
                            <FontAwesome name="twitter" size={32} color="#1DA1F2" />
                        </Button>
                    </XStack>
                    <YStack mt="$4" ai="center">
                        <Text color="$color">Don't have an account? </Text>
                        <Link href="/auth/register">
                            <Text color="$primaryColor" fontWeight="bold">
                                Sign Up
                            </Text>
                        </Link>
                    </YStack>
                </YStack>
            </YStack>
        )
    }
}
