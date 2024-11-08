import React, { Component } from 'react'
import { Button, Input, Stack, YStack, Text, XStack, SizableText } from 'tamagui'
import { Link } from 'expo-router'
import { FontAwesome } from '@expo/vector-icons'
import axios from 'axios'

interface RegisterState {
    name: string
    email: string
    password: string
    showPassword: boolean
    nameError: string
    emailError: string
    passwordError: string
    passwordStrength: 'weak' | 'medium' | 'strong' | ''
    showPasswordHint: boolean
    registerError: string
}

export default class RegisterScreen extends Component<{}, RegisterState> {
    constructor(props: {}) {
        super(props)
        this.state = {
            name: '',
            email: '',
            password: '',
            showPassword: false,
            nameError: '',
            emailError: '',
            passwordError: '',
            passwordStrength: '',
            showPasswordHint: false,
            registerError: ''
        }
    }

    handleRegister = async () => {
        const { name, email, password } = this.state

        // Clear previous errors
        this.setState({ nameError: '', emailError: '', passwordError: '', registerError: '' })

        // Validate inputs
        let isValid = true
        if (name.trim() === '') {
            this.setState({ nameError: 'Name is required' })
            isValid = false
        }
        if (!/^[\w-.]+@([\w-]+\.)+[\w-]{2,4}$/.test(email)) {
            this.setState({ emailError: 'Invalid email address' })
            isValid = false
        }
        if (password.length < 6) {
            this.setState({ passwordError: 'Password must be at least 6 characters' })
            isValid = false
        }

        if (!isValid) return

        // Submit data if validation passes
        try {
            const response = await axios.post('YOUR_BACKEND_API_URL/register', {
                name,
                email,
                password,
            })
            console.log('Registration successful:', response.data)
            // Handle successful registration, e.g., navigate to login
        } catch (error) {
            //console.log('Registration failed:', error)
            this.setState({
                registerError: 'Registration failed: ' + String(error),
            })
        }
    }

    validateEmailFormat = (email: string) => {
        const emailRegex = /^[\w-.]+@([\w-]+\.)+[\w-]{2,4}$/
        this.setState({ emailError: emailRegex.test(email) ? '' : 'Invalid email address' })
    }

    validatePasswordStrength = (password: string) => {
        let level = 0;
        if (password.length >= 6 && password.length < 8)
            level++;
        if (password.length >= 8)
            level++;
        if (/\d/.test(password))
            level++;
        if (/[A-Z]/.test(password))
            level++;
        if (/[a-z]/.test(password))
            level++;
        if (/[\.!@#$%^&*]/.test(password))
            level++;

        if (level <= 2)
            return "weak"
        else if (level >= 3 && level <= 4)
            return "medium"
        else if (level >= 5)
            return "strong"

        return 'weak'
    }

    handlePasswordChange = (password: string) => {
        this.setState({
            password,
            passwordStrength: this.validatePasswordStrength(password)
        })
    }

    togglePasswordVisibility = () => {
        this.setState((prevState) => ({
            showPassword: !prevState.showPassword,
        }))
    }

    togglePasswordHint = () => {
        this.setState((prevState) => ({
            showPasswordHint: !prevState.showPasswordHint,
        }))
    }

    handleSocialLogin = (provider: string) => {
        console.log(`Logging in with ${provider}`)
        // Handle social login logic for each provider
    }

    render() {
        const {
            name, email, password, showPassword, nameError, emailError, passwordError,
            passwordStrength, showPasswordHint, registerError
        } = this.state

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
                    width="90%"
                    alignSelf="center"
                    gap="$4"
                    ai="center"
                >
                    <Text fontSize={24} fontWeight="bold" color="$color" mb="$4">
                        Register
                    </Text>
                    <Stack gap="$4" width="100%">
                        <Input
                            placeholder="Name"
                            value={name}
                            onChangeText={(text) => this.setState({ name: text })}
                        />
                        {nameError ? <Text color="red" fontSize={14}>{nameError}</Text> : null}

                        <Input
                            placeholder="Email"
                            value={email}
                            onChangeText={(text) => this.setState({ email: text })}
                            onBlur={() => this.validateEmailFormat(email)}
                            keyboardType="email-address"
                            autoCapitalize="none"
                        />
                        {emailError ? <Text color="red" fontSize={14}>{emailError}</Text> : null}

                        <XStack ai="center" gap="$2">
                            <Input
                                placeholder="Password"
                                value={password}
                                onChangeText={this.handlePasswordChange}
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
                            <Button onPress={this.togglePasswordHint}>
                                <FontAwesome
                                    name="info-circle"
                                    size={20}
                                    color="$color"
                                />
                            </Button>
                        </XStack>
                        {/* Password Hint */}
                        {showPasswordHint && (
                            <Text color="$color" fontSize={14} mt="$2">
                                Use at least 8 characters, with an uppercase letter, a number, and a special symbol (.!@#$%^&*).
                            </Text>
                        )}

                        {/* Password Strength Text */}
                        <Text fontSize={14} color={
                            passwordStrength === 'strong' ? 'green' :
                                passwordStrength === 'medium' ? 'orange' :
                                    passwordStrength === 'weak' ? 'red' : '$color'
                        }>
                            Password Strength: {passwordStrength || 'Enter a password'}
                        </Text>

                        {/* Password Strength Indicator */}
                        <XStack width="100%" height={5} bg="$gray3" borderRadius={5} overflow="hidden" mt="$2">
                            <YStack flex={1} bg={passwordStrength === 'weak' ? 'red' : '$gray3'} />
                            <YStack flex={1} bg={passwordStrength === 'medium' ? 'orange' : '$gray3'} />
                            <YStack flex={1} bg={passwordStrength === 'strong' ? 'green' : '$gray3'} />
                        </XStack>

                        {/* Password error message */}
                        {passwordError ? <Text color="red" fontSize={14}>{passwordError}</Text> : null}

                        <Button onPress={this.handleRegister}>
                            <Text color="$color">Register</Text>
                        </Button>

                        {/* Password error message */}
                        {registerError ? <Text color="red" fontSize={14}>{registerError}</Text> : null}
                    </Stack>

                    <Text color="$color" mt="$4" mb="$2">
                        Or register with:
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
                        <Text color="$color">Already have an account? </Text>
                        <Link href="/auth/login">
                            <Text color="$primaryColor" fontWeight="bold">
                                Log In
                            </Text>
                        </Link>
                    </YStack>
                </YStack>
            </YStack>
        )
    }
}
