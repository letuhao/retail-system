import { Link } from 'expo-router'
import { Text, View } from 'tamagui'

export default function TabTwoScreen() {
    return (
        <View flex={1} alignItems="center" justifyContent="center" bg="$background">
            <Text fontSize={20} color="$blue10">
                Tab Two
            </Text>
            <Link href="/auth/login">Go to Login Page</Link>
            <Link href="/shops/create">Go to Shop Create</Link>
        </View>
    )
}
