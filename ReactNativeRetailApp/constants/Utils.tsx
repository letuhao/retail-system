import { Platform, StatusBar, Dimensions } from 'react-native';
import { theme } from 'galio-framework';

const { height, width } = Dimensions.get('screen');

class Utils {
    static StatusHeight = StatusBar.currentHeight;
    static HeaderHeight = (theme.SIZES?.BASE ?? 0 * 3.5 + (Utils.StatusHeight || 0));
    static iPhoneX = () => Platform.OS === 'ios' && (height === 812 || width === 812);
}

export default Utils;