import React, { Component } from 'react'
import { Button, Input, Stack, YStack, Text, XStack } from 'tamagui'
import { Image, Platform } from 'react-native'
import * as ImagePicker from 'expo-image-picker'
import axios from 'axios'

interface CreateShopState {
    name: string
    description: string
    phoneNumber: string
    address: string
    imageFiles: string[]
    nameError: string
    descriptionError: string
    phoneNumberError: string
    addressError: string
    imageError: string
    createError: string
}

export default class CreateShopScreen extends Component<{}, CreateShopState> {
    constructor(props: {}) {
        super(props)
        this.state = {
            name: '',
            description: '',
            phoneNumber: '',
            address: '',
            imageFiles: [],
            nameError: '',
            descriptionError: '',
            phoneNumberError: '',
            addressError: '',
            imageError: '',
            createError: ''
        }
    }

    handleCreateShop = async () => {
        const { name, description, phoneNumber, address, imageFiles } = this.state

        // Clear previous errors
        this.setState({ nameError: '', descriptionError: '', phoneNumberError: '', addressError: '', imageError: '', createError: '' })

        // Validate inputs
        let isValid = true
        if (name.trim() === '') {
            this.setState({ nameError: 'Name is required' })
            isValid = false
        }
        if (description.trim() === '') {
            this.setState({ descriptionError: 'Description is required' })
            isValid = false
        }
        if (!/^\+?[0-9]{7,15}$/.test(phoneNumber)) {
            this.setState({ phoneNumberError: 'Invalid phone number' })
            isValid = false
        }
        if (address.trim() === '') {
            this.setState({ addressError: 'Address is required' })
            isValid = false
        }
        if (imageFiles.length === 0) {
            this.setState({ imageError: 'At least one image is required' })
            isValid = false
        }

        if (!isValid) return

        try {
            const formData = new FormData()
            formData.append('name', name)
            formData.append('description', description)
            formData.append('phoneNumber', phoneNumber)
            formData.append('address', address)
            imageFiles.forEach((uri, index) => {
                formData.append('images', {
                    uri,
                    type: 'image/jpeg',
                    name: `image${index}.jpg`,
                } as any)
            })

            const response = await axios.post('YOUR_BACKEND_API_URL/create-shop', formData, {
                headers: { 'Content-Type': 'multipart/form-data' },
            })
            console.log('Shop created successfully:', response.data)
            // Handle success (e.g., navigate to the shop list page)
        } catch (error) {
            // console.error('Failed to create shop:', error)
            this.setState({
                createError: 'Failed to create shop: ' + String(error),
            })
        }
    }

    handlePickImages = async () => {
        const result = await ImagePicker.launchImageLibraryAsync({
            mediaTypes: ImagePicker.MediaTypeOptions.Images,
            allowsMultipleSelection: true,
        })

        if (!result.canceled) {
            const selectedImages = result.assets?.map((asset) => asset.uri) || []
            this.setState({ imageFiles: selectedImages, imageError: '' })
        }
    }

    render() {
        const {
            name, description, phoneNumber, address, imageFiles,
            nameError, descriptionError, phoneNumberError, addressError, imageError, createError
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
                        Create Shop
                    </Text>
                    <Stack gap="$4" width="100%">
                        <Input
                            placeholder="Shop Name"
                            value={name}
                            onChangeText={(text) => this.setState({ name: text })}
                        />
                        {nameError ? <Text color="red" fontSize={14}>{nameError}</Text> : null}

                        <Input
                            placeholder="Description"
                            value={description}
                            onChangeText={(text) => this.setState({ description: text })}
                            multiline
                        />
                        {descriptionError ? <Text color="red" fontSize={14}>{descriptionError}</Text> : null}

                        <Input
                            placeholder="Phone Number"
                            value={phoneNumber}
                            onChangeText={(text) => this.setState({ phoneNumber: text })}
                            keyboardType="phone-pad"
                        />
                        {phoneNumberError ? <Text color="red" fontSize={14}>{phoneNumberError}</Text> : null}

                        <Input
                            placeholder="Address"
                            value={address}
                            onChangeText={(text) => this.setState({ address: text })}
                        />
                        {addressError ? <Text color="red" fontSize={14}>{addressError}</Text> : null}

                        <Button onPress={this.handlePickImages}>
                            <Text color="$color">Choose Image(s)</Text>
                        </Button>
                        {imageError ? <Text color="red" fontSize={14}>{imageError}</Text> : null}

                        {/* Display selected images */}
                        <XStack gap="$2" mt="$2" flexWrap="wrap">
                            {imageFiles.map((uri, index) => (
                                <Image
                                    key={index}
                                    source={{ uri }}
                                    style={{ width: 80, height: 80, borderRadius: 8 }}
                                />
                            ))}
                        </XStack>

                        <Button onPress={this.handleCreateShop}>
                            <Text color="$color">Create Shop</Text>
                        </Button>

                        {createError ? <Text color="red" fontSize={14}>{createError}</Text> : null}
                    </Stack>
                </YStack>
            </YStack>
        )
    }
}
